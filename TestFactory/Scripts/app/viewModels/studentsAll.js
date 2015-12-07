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
    //self.subjectsMarks = ko.observableArray();

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

    self.listMarks = function (forSubject) {
        ko.dependentObservable(function () {
            var arrOfMarks;
            if (forSubject) {
                arrOfMarks = ko.utils.arrayMap(self.filteredRecords2(), function (item) {
                    return item.subjectMarks()
                });
            }
            else {
                arrOfMarks = ko.utils.arrayMap(self.filteredRecords2(), function (item) {
                    return item.marks()
                });
            }

            var averageArrOfMarks = [];
            if (arrOfMarks.length > 0) {
                //array 100 *n(categories mark) elements:
                var standardDeviation = new Array();
                for (var i = 0; i < arrOfMarks[0].length; i++) {
                    standardDeviation[i] = new Array();
                }

                var forAverage = 0;

                for (var j = 0; j < arrOfMarks.length ; j++) {
                    for (var i = 0; i < arrOfMarks[j].length ; i++) {

                        if (averageArrOfMarks[i] == undefined) averageArrOfMarks[i] = arrOfMarks[j][i].value();
                        else
                            averageArrOfMarks[i] += arrOfMarks[j][i].value();

                        var cell = standardDeviation[i][arrOfMarks[j][i].value()];
                        if (cell == undefined) standardDeviation[i][arrOfMarks[j][i].value()] = 1;
                        else
                            standardDeviation[i][arrOfMarks[j][i].value()]++;
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
                        averageArrOfMarks[i] = averageArrOfMarks[i] / arrOfMarks.length;
                    }

                    //push average marks
                    if (!forSubject) {
                        var averageMarkConteiner = $(".averagemarks");
                        for (var i = 0; i < averageArrOfMarks.length; i++) {
                            var aver = averageArrOfMarks[i];
                            var conteinerHTML = averageMarkConteiner.eq(0).html();
                            if (aver != null) {
                                conteinerHTML += "<div class='marks center averageMarks'>" + aver.toFixed(2) + "</div>";
                                averageMarkConteiner.eq(0).html(conteinerHTML);
                            }
                        }
                    }
                    else {
                        var averageMarkConteiner = $(".averageSubjectMarks");
                        for (var i = 0; i < averageArrOfMarks.length; i++) {
                            var aver = averageArrOfMarks[i];
                            var conteinerHTML = averageMarkConteiner.eq(0).html();
                            if (aver != null) {
                                conteinerHTML += "<div class='marks center averSubjectMarks subjectAverageContent'>" + aver.toFixed(2) + "</div>";
                                averageMarkConteiner.eq(0).html(conteinerHTML);
                            }
                        }
                    }

                    //push mathematical expectation marks
                    if (!forSubject) {
                        var mathematicalExpectationContainer = $(".mathematicalExpectation");
                        for (var i = 0 ; i < dispersion.length; i++) {
                            var conteinerHTML = mathematicalExpectationContainer.eq(0).html();
                            if (dispersion[i] != null) {
                                conteinerHTML += "<div class='mathematicalExpectationMarks center'>" + Math.sqrt(dispersion[i]).toFixed(2) + "</div>";
                                mathematicalExpectationContainer.eq(0).html(conteinerHTML);
                            }
                        }
                    }
                    else {
                        var mathematicalExpectationContainer = $(".mathematicalExpectationSublectMarks");
                        for (var i = 0 ; i < dispersion.length; i++) {
                            var conteinerHTML = mathematicalExpectationContainer.eq(0).html();
                            if (dispersion[i] != null) {
                                conteinerHTML += "<div class='mathematicalExpectationMarks center subjectAverageContent'>" + Math.sqrt(dispersion[i]).toFixed(2) + "</div>";
                                mathematicalExpectationContainer.eq(0).html(conteinerHTML);
                            }
                        }
                    }
                }

                //recalculate average marks deviation
                if (!forSubject) {
                    var averageMarks = $(".averageMarks");
                    var studentMarkForCategories = $(".studentMark");

                    for (var i = 0; i < studentMarkForCategories.length; i++) {
                        var value = studentMarkForCategories.eq(i).html() - averageMarks.eq(i % averageMarks.length).html();
                        studentMarkForCategories.eq(i).parents(".markSubjectContent").find(".greenText").html(value.toFixed(2));
                        if (value >= 0) {
                            studentMarkForCategories.eq(i).parents(".markSubjectContent").find(".greenText").css("color", "green");
                        }
                        else
                            studentMarkForCategories.eq(i).parents(".markSubjectContent").find(".greenText").css("color", "coral");
                    }
                }
            else
            {
                var averageMarks = $(".averSubjectMarks");
                var studentMarkForCategories = $(".subjectMark");

                for (var i = 0; i < studentMarkForCategories.length; i++) {
                    var value = studentMarkForCategories.eq(i).html() - averageMarks.eq(i % averageMarks.length).html();
                    studentMarkForCategories.eq(i).parents(".markSubjectContent").find(".greenText").html(value.toFixed(2));
                    if (value >= 0) {
                        studentMarkForCategories.eq(i).parents(".markSubjectContent").find(".greenText").css("color", "green");
                    }
                    else
                        studentMarkForCategories.eq(i).parents(".markSubjectContent").find(".greenText").css("color", "coral");
                }
            }
            }
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
            self.listMarks(true);
        });
        
        self.sortingByName(sortingBy);
    }

    

    self.init();
}