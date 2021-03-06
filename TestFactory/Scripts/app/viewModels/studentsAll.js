﻿function  StudentsAllViewModel(sortingBy) {
    var self = this;

    var studentProvider = new StudentProvider();
    var markProvider = new MarkProvider();
    var groupProvider = new GroupProvider();
    var subjectProvider = new SubjectProvider();
    var subjectMarkProvider = new SubjectMarkProvider();


    //todo: move urls to providers
    var pathForMarProvider= {
        everage: "/average",
        deviation: "/standardDeviation"
    };

    var pathForGroupProvider = {
        name: "/name",
        year:"/year"
    };

    self.students = ko.observableArray();

    self.groupNames = ko.observableArray();
    
    self.groupYears = ko.observableArray();

    self.subjects = ko.observableArray();

    self.correlationsMarks = ko.observableArray();

    self.searchByStudentsGroups = ko.observable('');

    self.RecalculateAverageMarksArrayHelper = ko.observableArray();

    self.RecalculateAverageMarksArray = ko.observableArray();

    self.searchByStudentsYearOfStartEducation = ko.observable('');

    self.filteredRecords = ko.computed(function () {
        return ko.utils.arrayFilter(self.students(), function (rec) {
            return (
                      (self.searchByStudentsYearOfStartEducation() == null || rec.year() == self.searchByStudentsYearOfStartEducation())
                   )
        });
    });
    
    self.uniqueGroupNames = ko.dependentObservable(function() {
        var groups = ko.utils.arrayMap(self.filteredRecords(), function(item){ return item.groupShortName()})
        return ko.utils.arrayGetDistinctValues(groups).sort();
    });

    self.filteredRecords2 = ko.computed(function () {
        return ko.utils.arrayFilter(self.filteredRecords(), function (rec) {
            return (
                      (self.searchByStudentsGroups() == null || self.searchByStudentsGroups().length == 0 || rec.groupShortName().toLowerCase().indexOf(self.searchByStudentsGroups().toLowerCase()) > -1)
                   )
        });
    });

    self.resetDropdowns = function () {
        self.searchByStudentsYearOfStartEducation(null);
        self.searchByStudentsGroups(null);
    }

    self.categories = ko.observableArray();
    self.standardDeviation = ko.observableArray();

    self.sortDescending = ko.observable(false);
    self.sortKey = ko.observable();
    self.preloader = ko.observable(true);

    self.sortingByMark = function (key, categoryId, code) {
        self.sortKey(code());
        if (self.sortDescending()) {
            self.students.sort(function (left, right) {
                return compareMarksModelDesc(left, right, categoryId);
            });
            self.sortDescending(false);
        } else {
            self.students.sort(function (left, right) {
                return compareMarksModelAsc(left, right, categoryId);
            });
            self.sortDescending(true);
        }

    }

    self.listMarks = function () {
        ko.dependentObservable(function () {

            var calculateStatistics = function(arrOfMarks)
            {
                var averageArrOfMarks = [];
                if (arrOfMarks.length > 0) {
                    //array 100 *n(categories mark) elements:
                    var standardDeviation = new Array();
                    for (var i = 0; i < arrOfMarks[0].length; i++) {
                        standardDeviation[i] = new Array();
                    }

                    var forAverage = 0;
                    var notValid = new Array();

                    for (var j = 0; j < arrOfMarks.length ; j++) {
                        for (var i = 0; i < arrOfMarks[j].length ; i++) {
                            if (arrOfMarks[j][i].value() != '-') {
                                if (averageArrOfMarks[i] == undefined) averageArrOfMarks[i] = arrOfMarks[j][i].value();
                                else
                                    averageArrOfMarks[i] += arrOfMarks[j][i].value();

                                var cell = standardDeviation[i][arrOfMarks[j][i].value()];
                                if (cell == undefined) standardDeviation[i][arrOfMarks[j][i].value()] = 1;
                                else
                                    standardDeviation[i][arrOfMarks[j][i].value()]++;
                            }
                            else
                            {
                                if (notValid[i] == undefined) {
                                    notValid[i] = 0;
                                }
                                else
                                    notValid[i]++;
                            }
                        }
                    }

                    var dispersion = [];
                    var mathematicalExpectation = [];
                    var standardDeviationCount = [];

                    for (var i = 0; i < standardDeviation.length; i++) {
                        for (var j = 0; j < standardDeviation[i].length; j++) {
                            if (standardDeviation[i][j] != undefined && standardDeviationCount[i] != undefined) standardDeviationCount[i] += standardDeviation[i][j];
                            else
                                if (standardDeviation[i][j] != undefined) standardDeviationCount[i] = standardDeviation[i][j];
                        }
                    }

                    for (var i = 0; i < standardDeviation.length; i++) {
                        for (var j = 0; j < standardDeviation[i].length; j++) {
                            if (standardDeviation[i][j] != undefined) {
                                standardDeviation[i][j] = standardDeviation[i][j] / standardDeviationCount[i];
                                if (mathematicalExpectation[i] == undefined) mathematicalExpectation[i] = standardDeviation[i][j] * j;
                                else
                                    mathematicalExpectation[i] += standardDeviation[i][j] * j;

                                if (dispersion[i] == undefined) dispersion[i] = standardDeviation[i][j] * j * j;
                                else
                                    dispersion[i] += standardDeviation[i][j] * j * j;
                            }
                        }
                    }

                    for (var i = 0; i < dispersion.length; i++) {
                        dispersion[i] = dispersion[i] - mathematicalExpectation[i] * mathematicalExpectation[i];
                    }



                    if (arrOfMarks.length != null && arrOfMarks.length != 0) {
                        for (var i = 0; i < averageArrOfMarks.length; i++) {
                            if (notValid[i] != undefined) averageArrOfMarks[i] = averageArrOfMarks[i] / (arrOfMarks.length - notValid[i]);
                            else
                                averageArrOfMarks[i] = averageArrOfMarks[i] / arrOfMarks.length;
                        }

                        var data = new Object();
                        data.averageArrOfMarks = averageArrOfMarks;
                        data.dispersion = dispersion;
                        data.mathematicalExpectation = mathematicalExpectation;
                        data.standardDeviation = standardDeviation;
                        data.standardDeviationCount = standardDeviationCount;

                        return data;
                    }
                }
            }

            var pushMarks = function (data, forSubject) {

                var recalculateAverageMarksDeviation = function (averageMarksContainer, thisMarksContainer) {
                    var averageMarks = $("." + averageMarksContainer);
                    var studentMarkForCategories = $("." + thisMarksContainer);
                    for (var i = 0; i < studentMarkForCategories.length; i++) {
                        var value = studentMarkForCategories.eq(i).html() - averageMarks.eq(i % averageMarks.length).html();

                        if (!isNaN(value)) studentMarkForCategories.eq(i).parents(".markSubjectContent").find(".greenText").html(value.toFixed(2));
                        else
                            studentMarkForCategories.eq(i).parents(".markSubjectContent").find(".greenText").html(".");

                        if (value >= 0) {
                            studentMarkForCategories.eq(i).parents(".markSubjectContent").find(".greenText").css("color", "green");
                        }
                        else
                            studentMarkForCategories.eq(i).parents(".markSubjectContent").find(".greenText").css("color", "coral");
                    }
                }

                if (!forSubject) {
                    //push average marks
                    var averageMarkConteiner = $(".averagemarks");
                    var averageMarksName = $(".averagemarks .averageMarksNameColumn").html();
                    averageMarkConteiner.eq(0).html('<div class="averageMarksNameColumn">' + averageMarksName + '</div>');
                    for (var i = 0; i < data.averageArrOfMarks.length; i++) {
                        var aver = data.averageArrOfMarks[i];
                        var conteinerHTML = averageMarkConteiner.eq(0).html();
                        if (aver != null) {
                            conteinerHTML += "<div class='marks center averageMarks'>" + aver.toFixed(2) + "</div>";
                            averageMarkConteiner.eq(0).html(conteinerHTML);
                        }
                    }

                    //push mathematical expectation marks
                    var mathematicalExpectationContainer = $(".mathematicalExpectation");
                    var mathematicalExpectationName = $(".mathematicalExpectation .mathematicalExpectationNameColumn").html();
                    mathematicalExpectationContainer.eq(0).html('<div class="mathematicalExpectationNameColumn">' + mathematicalExpectationName + '</div>');
                    for (var i = 0 ; i < data.dispersion.length; i++) {
                        var conteinerHTML = mathematicalExpectationContainer.eq(0).html();
                        if (data.dispersion[i] != null) {
                            conteinerHTML += "<div class='mathematicalExpectationMarks center'>" + Math.sqrt(data.dispersion[i]).toFixed(2) + "</div>";
                            mathematicalExpectationContainer.eq(0).html(conteinerHTML);
                        }
                    }

                    //recalculate average marks deviation
                    recalculateAverageMarksDeviation("averageMarks", "studentMark");
                }
                else {
                    //push average marks
                    var averageMarkConteiner = $(".averageSubjectMarks");
                    var conteinerHTML = "";
                    for (var i = 0; i < data.averageArrOfMarks.length; i++) {
                        var aver = data.averageArrOfMarks[i];
                        //var conteinerHTML = "";
                        if (aver != null) {
                            conteinerHTML += "<div class='marks center averSubjectMarks subjectAverageContent'>" + aver.toFixed(2) + "</div>";
                            averageMarkConteiner.eq(0).html(conteinerHTML);
                        }
                    }

                    //push mathematical expectation marks
                    var mathematicalExpectationContainer = $(".mathematicalExpectationSublectMarks");
                    var conteinerHTML = "";
                    for (var i = 0 ; i < data.dispersion.length; i++) {

                        if (data.dispersion[i] != null) {
                            conteinerHTML += "<div class='mathematicalExpectationMarks center subjectAverageContent'>" + Math.sqrt(data.dispersion[i]).toFixed(2) + "</div>";
                            mathematicalExpectationContainer.eq(0).html(conteinerHTML);
                        }
                    }

                    //recalculate average marks deviation
                    recalculateAverageMarksDeviation("averSubjectMarks", "subjectMark");
                }
            }

            var CalculateCorrelation = function (data1, data2, data3, data4) {
                self.correlationsMarks.removeAll();
                var sumX = 0;
                var sumX2 = 0;
                var sumY = 0;
                var sumY2 = 0;
                var sum = 0;
                var corelation = [];
                if (data3.length < 2) {
                    return;
                }
                for (var k = 0; k < data2.averageArrOfMarks.length; k++) {
                    corelation = [];
                    for (var j = 0; j < data1.averageArrOfMarks.length; j++) {
                        sumX = 0;
                        sumX2 = 0;
                        sumY = 0;
                        sumY2 = 0;
                        sum = 0;
                        for (var i = 0; i < data3.length; i++) {
                            var x = data3[i][k].value();
                            var xAverage = data2.averageArrOfMarks[k];
                            var y = data4[i][j].value();
                            var yAverage = data1.averageArrOfMarks[j];
                            if (x == "-") {
                                x = xAverage;
                }
                            sumX = (x - xAverage);
                            sumX2 += (sumX * sumX);
                            sumY = (y - yAverage);
                            sumY2 += (sumY * sumY);
                            sum += sumX * sumY;
                        }
                        corelation.push((sum / Math.sqrt(sumX2 * sumY2)).toFixed(3));
                        }
                    self.correlationsMarks.push(corelation);
                            }
                
                        }




            var arrOfMarksSubject = ko.utils.arrayMap(self.filteredRecords2(), function (item) {
                return item.subjectMarks()
            });
                
            var dataSubjectMarks = calculateStatistics(arrOfMarksSubject);
            pushMarks(dataSubjectMarks, true);



            var arrOfMarksTest = ko.utils.arrayMap(self.filteredRecords2(), function (item) {
                return item.marks()
            });

            var dataTestMarks = calculateStatistics(arrOfMarksTest);
            pushMarks(dataTestMarks);

            CalculateCorrelation(dataTestMarks, dataSubjectMarks, arrOfMarksSubject, arrOfMarksTest);
        });
    }

    self.sortingByName = function (key) {
        self.sortKey(key);
        if (self.sortDescending()) {
            self.students.sort(function (left, right) {
                return compareByKeyDesc(left, right, key);
            });
            self.sortDescending(false);
        } else {
            self.students.sort(function (left, right) {
                return compareByKeyAsc(left, right, key);
            });
            self.sortDescending(true);
        }

    }

    self.selectedClassForSortedField = ko.pureComputed(function () {
        return self.sortDescending() ? "triangle-up" : "triangle-down";
    }, self);

    self.init = function () {
        
        //todo: move urls to providers
        markProvider.get(pathForMarProvider.everage, function (data) {
            $(data).each(function (index, element) {
                
                var mappedItem = new AverageMarksForFacultyModel(element);
                self.categories.push(mappedItem);
            });
        });       

        groupProvider.getNames(pathForGroupProvider.name, function (data) {
            $(data).each(function (index, element) {
                self.groupNames.push(element);
            });
        });

        groupProvider.getYears(pathForGroupProvider.year, function (data) {
            $(data).each(function (index, element) {
                self.groupYears.push(element);
            });
        });

        subjectProvider.getAll(function (data) {
            $(data).each(function (index, element) {
                self.subjects.push(element);
            });
        });

        //subjectMarkProvider.getAll(function (data) {
        //    $(data).each(function (index, element) {
        //        self.subjectsMarks.push(element);
        //    });
        //});

        studentProvider.get(function (data) {
            $(data).each(function (index, element) {
                var mappedStudent = new StudentForAllModel(element);
                mappedStudent.sortMarksByCategoryIdDesc();
                
                self.students.push(mappedStudent);
            });
            self.preloader(false);
            self.listMarks();
        });
        
        self.sortingByName(sortingBy);
    }

    

    self.init();
}