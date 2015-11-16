﻿function  StudentsAllViewModel(sortingBy) {
    var self = this;

    var studentProvider = new StudentProvider();
    var markProvider = new MarkProvider();
    var pathForMarProvider= {
        everage: "/average",
        deviation: "/standardDeviation"
    };

    self.myViewModel = {
        groupNames: (['ПІ 52', 'ПІ 51', 'ПІК 12', 'КІ 1']),
        years:(['2015',2010])
    };

    self.students = ko.observableArray();
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
        var averageMarks = [];
        markProvider.get(pathForMarProvider.everage, function (data) {
            $(data).each(function (index, element) {
                averageMarks[index] = element;
                var mappedItem = new AverageMarksForFacultyModel(element);
                self.categories.push(mappedItem);
            });
        });
        markProvider.get(pathForMarProvider.deviation, function (data) {
            $(data).each(function (index, element) {
                self.standardDeviation.push(element);
            });
        });

        studentProvider.get(function (data) {
            $(data).each(function (index, element) {
                var mappedStudent = new StudentForAllModel(element, averageMarks);
                mappedStudent.sortMarksByCategoryIdDesc();
                self.students.push(mappedStudent);
            });
            self.preloader(false);
        });
        self.sortingByName(sortingBy);
    }

    self.init();
}