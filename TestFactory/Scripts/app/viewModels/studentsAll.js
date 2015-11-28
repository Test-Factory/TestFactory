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

    self.subjectsMarks = ko.observableArray();

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
            var arrOfMarks = ko.utils.arrayMap(self.filteredRecords2(), function (item) {
                return item.marks()
            });
            var averageArrOfMarks = [];
            var standardDeviation = [];
            var forAverage = 0;

            for (var j = 0; j < arrOfMarks.length ; j++) {
                for (var i = 0; i < arrOfMarks[j].length ; i++) {
                    if (j == 0) averageArrOfMarks[i] = arrOfMarks[j][i].value._latestValue;
                    else
                        averageArrOfMarks[i] += arrOfMarks[j][i].value._latestValue;
                }
            }
            if (arrOfMarks.length != null) {
                for (var i = 0; i < averageArrOfMarks.length; i++) {
                    averageArrOfMarks[i] = averageArrOfMarks[i] / arrOfMarks.length;
                }
                var averageMarkConteiner = $(".averagemarks");
                var averageMarksName = $(".averagemarks .averageMarksNameColumn").html();
                averageMarkConteiner.eq(0).html('<td colspan="3" class="averageMarksNameColumn">' + averageMarksName + '</td>');
                for (var i = 0; i < averageArrOfMarks.length; i++) {
                    var aver = averageArrOfMarks[i];
                    var conteinerHTML = averageMarkConteiner.eq(0).html();
                    if (aver != null) {
                        conteinerHTML += "<td class='marks center'>" + aver.toFixed(2) + "</td>";
                        averageMarkConteiner.eq(0).html(conteinerHTML);
                    }
                }
                console.log(averageMarkConteiner.eq(0).html())
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

        subjectMarkProvider.getAll(function (data) {
            $(data).each(function (index, element) {
                self.subjectsMarks.push(element);
            });
        });

        studentProvider.get(function (data) {
            $(data).each(function (index, element) {
                var mappedStudent = new StudentForAllModel(element, averageMarks);
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