function  StudentsAllViewModel(sortingBy) {
    var self = this;

    var studentProvider = new StudentProvider();
    var markProvider = new MarkProvider();
    var groupProvider = new GroupProvider();

    //todo: move urls to providers
    var pathForMarProvider= {
        everage: "/average",
        deviation: "/standardDeviation"
    };

    self.temporary = {
        groupNames: ([' ', 'ПІ 52', 'ПІ 51', 'ПІК 13', 'КІ 1']),
        years:(['2015','2014'])
    };

    self.students = ko.observableArray();

    self.groups = ko.observableArray();

    self.searchByStudentsGroups = ko.observable('');

    self.searchByStudentsYearOfStartEducation = ko.observable('');

    self.filteredRecords = ko.computed(function () {
        return ko.utils.arrayFilter(self.students(), function (rec) {
            return (
                      (self.searchByStudentsGroups().length == 0 || rec.groupShortName().toLowerCase().indexOf(self.searchByStudentsGroups().toLowerCase()) > -1)
                &&
                      (self.searchByStudentsYearOfStartEducation().length == 0 || rec.Year().toLowerCase().indexOf(self.searchByStudentsYearOfStartEducation().toLowerCase()) > -1)
                   )
        });
    });

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
        //todo: move urls to providers
        markProvider.get(pathForMarProvider.everage, function (data) {
            $(data).each(function (index, element) {
                averageMarks[index] = element;
                var mappedItem = new AverageMarksForFacultyModel(element);
                self.categories.push(mappedItem);
            });
        });
        //todo: move urls to providers

        markProvider.get(pathForMarProvider.deviation, function (data) {
            $(data).each(function (index, element) {
                self.standardDeviation.push(element);
            });
        });

        groupProvider.get(function (data) {
            $(data).each(function (index, element) {
                var mappedGroup = new GroupModel(element);
                self.groups.push(mappedGroup);
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