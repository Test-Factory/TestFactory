﻿function  StudentsAllViewModel(sortingBy) {
    var self = this;

    var studentProvider = new StudentProvider();
    var markProvider = new MarkProvider();
    var pathForMarProvider= {
        everage: "/average",
        deviation: "/standardDeviation"
    };

    self.students = ko.observableArray();
    self.categories = ko.observableArray();
    self.standardDeviation = ko.observableArray();

    self.sortDescending = ko.observable(false);
    self.sortKey = ko.observable(sortingBy);
    self.preloader = ko.observable(true);

    self.sortingByMark = function (key, id, code) {
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
                sortStudentMarksByCategoryIdDesc(mappedStudent);
                self.students.push(mappedStudent);
            });
            self.preloader(false);
        });
        self.sortingByName("lastName");
    }

    self.init();

    function sortStudentMarksByCategoryIdDesc(student) {
        student.marks.sort(function (leftMark, rightMark) {
            if (leftMark.categoryId() == rightMark.categoryId())
                return 0;
            else if (leftMark.categoryId() < rightMark.categoryId())
                return -1;
            else
                return 1;
        });
    }
}