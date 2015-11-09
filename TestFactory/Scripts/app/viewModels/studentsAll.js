function  StudentsAllViewModel(parameters) {
    var self = this;

    var studentProvider = new StudentProvider();
    var markProvider = new MarkProvider();
    var firstSorting = "lastName";
    var pathForMarProvider= {
        everage: "/average",
        deviation: "/standardDeviation"
    };

    self.students = ko.observableArray();
    self.categories = ko.observableArray();
    self.standardDeviation = ko.observableArray();

    self.sortDescending = ko.observable(false);
    self.sortKey = ko.observable(firstSorting);
    self.preloader = ko.observable(true);

    self.sortingByMark = function (key, id, code) {
        self.sortKey(code());
        if (self.sortDescending()) {
            self.students.sort(function (left, right) {

                var getMark = function (item) {
                    return item.categoryId() == id();
                }
                var leftMark = ko.utils.arrayFilter(left.marks(), getMark)[0];
                var rightMark = ko.utils.arrayFilter(right.marks(), getMark)[0];

                if (leftMark.value() == rightMark.value())
                    return 0;
                else if (leftMark.value() < rightMark.value())
                    return 1;
                else
                    return -1;
            });
            self.sortDescending(false);
        } else {
            self.students.sort(function (left, right) {

                var getMark = function (item) {
                    return item.categoryId() == id();
                }
                var leftMark = ko.utils.arrayFilter(left.marks(), getMark)[0];
                var rightMark = ko.utils.arrayFilter(right.marks(), getMark)[0];

                if (leftMark.value() == rightMark.value())
                    return 0;
                else if (leftMark.value() < rightMark.value())
                    return -1;
                else
                    return 1;
            });
            self.sortDescending(true);
        }

    }

    self.sortingByName = function (key) {
        self.sortKey(key);
        if (self.sortDescending()) {
            self.students.sort(function (left, right) {

                if (left[key]().toUpperCase() == right[key]().toUpperCase())
                    return 0;
                else if (left[key]().toUpperCase() < right[key]().toUpperCase())
                    return 1;
                else
                    return -1;
            });
            self.sortDescending(false);
        } else {
            self.students.sort(function (left, right) {
                if (left[key]().toUpperCase() == right[key]().toUpperCase())
                    return 0;
                else if (left[key]().toUpperCase() < right[key]().toUpperCase())
                    return -1;
                else
                    return 1;
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
                //self.standardDeviation[index] = element;
                //var mappedItem = new AverageMarksForFacultyModel(element);
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