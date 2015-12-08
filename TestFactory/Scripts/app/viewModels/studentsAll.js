function  StudentsAllViewModel(sortingBy) {
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

            var CalculateCorrelation = function (data1, data2) {

                var correlations = new Array();
                for (var i = 0; i < data2.standardDeviation.length; i++) {
                    correlations[i] = new Array();
                }

                for (var i = 0; i < data1.standardDeviation.length; i++) {
                    for (var j = 0; j < data2.standardDeviation.length; j++) {
                        var distributionLaw = new Array();
                        for (var ii = 0; ii <data1.standardDeviation[i].length + 1; ii++) {
                            distributionLaw[ii] = new Array();
                        }

                        var k = 0;
                        for (var p = 0; p < data1.standardDeviation[i].length; p++) {
                            if (data1.standardDeviation[i][p] != undefined) {
                                k++;
                                distributionLaw[0][k] = p;
                                var kk = 0;
                                for (t = 0; t < data2.standardDeviation[j].length; t++) {
                                    if (data2.standardDeviation[j][t] != undefined) {
                                        kk++;
                                        distributionLaw[kk][0] = t;
                                        distributionLaw[kk][k] = data1.standardDeviation[i][p] * data2.standardDeviation[j][t];
                                    }
                                }
                            }
                        }
                        var correlationMark = 0;
                        for (var p = 1; p < distributionLaw.length; p++) {
                            for (var t = 1; t < distributionLaw[p].length; t++) {
                                correlationMark += distributionLaw[p][t] * distributionLaw[p][0] * distributionLaw[0][t];
                            }
                        }

                        correlationMark = (correlationMark - data1.mathematicalExpectation[i] * data2.mathematicalExpectation[j]) / Math.sqrt(data1.dispersion[i] * data2.dispersion[j]);

                        correlations[j][i] = correlationMark.toFixed(2);
                    }
                }

                $(correlations).each(function (index, element) {
                    self.correlationsMarks.push(element);
                });
            }

            var arrOfMarks;
            arrOfMarks = ko.utils.arrayMap(self.filteredRecords2(), function (item) {
                return item.subjectMarks()
            });
                
            var dataSubjectMarks = calculateStatistics(arrOfMarks);
            pushMarks(dataSubjectMarks, true);



            arrOfMarks = ko.utils.arrayMap(self.filteredRecords2(), function (item) {
                return item.marks()
            });

            var dataTestMarks = calculateStatistics(arrOfMarks);
            pushMarks(dataTestMarks);

            CalculateCorrelation(dataTestMarks, dataSubjectMarks); 
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