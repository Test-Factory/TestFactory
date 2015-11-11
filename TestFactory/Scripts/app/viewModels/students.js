﻿ko.bindingHandlers.tooltip = {
    update: function (element, valueAccessor) {
        var options = valueAccessor() || {};
        ko.utils.unwrapObservable(valueAccessor());
        if (options.message()) {
            $(element).tooltip("remove");
            $(element).attr('data-tooltip', options.message());
            $(element).tooltip({ delay: 0 });
        } else {
            $(element).tooltip("remove");
        }
    }
};

function StudentsViewModel(group, sortingBy) {

    var self = this;
    self.group = new GroupModel(group);
    self.sortDescending = ko.observable(false);

    var studentProvider = new StudentProvider(self.group.id);
    var categoryProvider = new CategoryProvider();

    self.students = ko.observableArray();
    self.categories = ko.observableArray();
    self.sortKey = ko.observable(sortingBy);

    self.studentForUpdate = ko.validatedObservable(new StudentModel(), { deep: true });
    self.studentForCreate = ko.validatedObservable(new StudentModel(), { deep: true });
    self.studentForDelete = ko.validatedObservable(new StudentModel(), { deep: true });

    self.mods = {
        display: "display",
        edit: "edit",
        deleting: "delete"
    };

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

    self.downloadReport = function (student) {
        var studentServerModel = toServerStudentModel(student);
        studentProvider.loadReport(studentServerModel, function () {
            console.log("result");
        });
    }

    self.editStudent = function (student) {
        closeAllEditing();
        mapStudent(student, self.studentForUpdate());
        self.studentForUpdate.valueHasMutated();

        student.mode(self.mods.edit);
    };

    self.deleteStudent = function (student) {
        $("#delete-student").dialog({
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Видалити": function () {
                    closeAllEditing();
                    mapStudent(student, self.studentForDelete());

                    self.categories.removeAll();
                    self.students.remove(student);

                    student.mode(self.mods.deleting);

                    var studentServerModel = toServerStudentModel(self.studentForDelete());
                    studentProvider.delete(studentServerModel, function () {
                        mapStudent(self.studentForDelete(), student);
                        student.mode(self.mods.deleting);
                    });
                    $(this).dialog("close");
                },
                Відмінити: function () {
                    $(this).dialog("close");
                }
            }
        });
    }

    self.addStudent = function () {
        closeAllEditing();
        mapStudent(new StudentModel(), self.studentForCreate());
        for (var c in self.categories()) {
            var mark = new MarkModel();
            mark.categoryId(self.categories()[c].id());
            self.studentForCreate().marks.push(mark);
        }
        self.studentForCreate().mode(self.mods.create);

        ko.validation.group(self.studentForCreate);
    };

    self.saveAddedStudent = function () {
        if (!self.studentForCreate.isValid()) {
            return false;
        }
        var studentServerModel = toServerStudentModel(self.studentForCreate());
        studentProvider.post(studentServerModel, function (data) {
            var newStudent = new StudentModel();
            mapStudent(self.studentForCreate(), newStudent);
            newStudent.id(data.Id);
            for (i in newStudent.marks()) {
                newStudent.marks()[i].studentId(newStudent.id());
                for (j in data.Marks) {
                    if (newStudent.marks()[i].categoryId() == data.Marks[j].CategoryId) {
                        newStudent.marks()[i].id(data.Marks[j].Id);
                        break;
                    }
                }
            }
            closeAllEditing();
            self.students.splice(0, 0, newStudent);
            self.addStudent();
        });
    };

    self.saveEditedStudent = function (student) {
        if (!self.studentForUpdate.isValid()) {
            return false;
        }
        var studentServerModel = toServerStudentModel(self.studentForUpdate());
        studentProvider.put(studentServerModel, function () {
            mapStudent(self.studentForUpdate(), student);
            student.mode(self.mods.display);
        });
    }
    self.init = function () {
        categoryProvider.get(function (data) {
            $(data).each(function (index, element) {
                var mappedItem = new CategoryModel(element);
                self.categories.push(mappedItem);
            });
            self.addStudent();
        });

        studentProvider.get(function (data) {
            $(data).each(function (index, element) {
                var mappedStudent = new StudentModel(element, self.mods.display);
                sortStudentMarksByCategoryIdDesc(mappedStudent);
                self.students.push(mappedStudent);
            });

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

    function closeAllEditing() {
        for (var k in self.students()) {
            self.students()[k].mode(self.mods.display);
        }
        self.studentForUpdate().mode(self.mods.display);
    }

    function toServerStudentModel(student) {
        return {
            Id: student.id(),
            FirstName: student.firstName(),
            LastName: student.lastName(),
            GroupId: self.group.id(),
            Marks: ko.utils.arrayMap(student.marks(), toServerMarkModel)
        }
    }

    function toServerMarkModel(mark) {
        return {
            Id: mark.id(),
            StudentId: mark.studentId(),
            CategoryId: mark.categoryId(),
            Value: mark.value()
        }
    }

    function mapStudent(from, to) {
        to.id(from.id());
        to.firstName(from.firstName());
        to.lastName(from.lastName());
        to.groupId(from.groupId());
        to.marks.removeAll();
        for (var m in from.marks()) {
            to.marks.push(from.marks()[m]);
        }
    }

}