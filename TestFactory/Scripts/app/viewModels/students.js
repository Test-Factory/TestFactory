ko.bindingHandlers.tooltip = {
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
    var subjectProvider = new SubjectProvider();

    self.students = ko.observableArray();
    self.subjects = ko.observableArray();
    self.categories = ko.observableArray();
    self.sortKey = ko.observable();
    self.preloader = ko.observable(true);

    self.studentForUpdate = ko.validatedObservable(new StudentModel(), { deep: true });
    self.studentForCreate = ko.validatedObservable(new StudentModel(), { deep: true });
    self.studentForDelete = ko.validatedObservable(new StudentModel(), { deep: true });
    self.subjectForCreate = ko.validatedObservable(new SubjectModel(), { deep: true });
    self.subjectForUpdate = ko.validatedObservable(new SubjectModel(), { deep: true });

    self.mods = {
        display: "display",
        edit: "edit",
        deleting: "delete"
    };

    self.sortingByMark = function(key, categoryId, code) {
        self.sortKey(code());
        if (self.sortDescending()) {
            self.students.sort(function(left, right) {
                return compareMarksModelDesc(left, right, categoryId);
            });
            self.sortDescending(false);
        } else {
            self.students.sort(function(left, right) {
                return compareMarksModelAsc(left, right, categoryId);
            });
            self.sortDescending(true);
        }

    }

    self.sortingByName = function(key) {
        self.sortKey(key);
        if (self.sortDescending()) {
            self.students.sort(function(left, right) {
                return compareByKeyDesc(left, right, key);
            });
            self.sortDescending(false);
        } else {
            self.students.sort(function(left, right) {
                return compareByKeyAsc(left, right, key);
            });
            self.sortDescending(true);
        }
    }

    self.selectedClassForSortedField = ko.pureComputed(function() {
        return self.sortDescending() ? "triangle-up" : "triangle-down";
    }, self);


    self.editStudent = function(student) {
        closeAllEditing();
        self.studentForUpdate().mapFrom(student);
        student.mode(self.mods.edit);
        //self.studentForUpdate.valueHasMutated();
    };

    self.deleteStudent = function(student) {
        $("#delete-student").dialog({
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Видалити": function() {
                    closeAllEditing();
                    self.studentForDelete().mapFrom(student);
                    self.categories.removeAll();
                    self.students.remove(student);

                    student.mode(self.mods.deleting);

                    var studentServerModel = self.studentForDelete().toServerModel(self.group.id());
                    studentProvider.delete(studentServerModel, function() {
                        student.mapFrom(self.studentForDelete());
                        student.mode(self.mods.deleting);
                    });
                    $(this).dialog("close");
                },
                Відмінити: function() {
                    $(this).dialog("close");
                }
            }
        });
    }

    self.addStudent = function() {
        closeAllEditing();
        var newStudent = new StudentModel();
        self.studentForCreate().mapFrom(newStudent);
        for (var c in self.categories()) {
            var mark = new MarkModel();
            mark.categoryId(self.categories()[c].id());
            self.studentForCreate().marks.push(mark);
        }
        self.studentForCreate().mode(self.mods.create);

        ko.validation.group(self.studentForCreate);
    };

    self.saveAddedStudent = function() {
        if (!self.studentForCreate.isValid()) {
            return false;
        }
        var studentServerModel = self.studentForCreate().toServerModel(self.group.id());
        studentProvider.post(studentServerModel, function(data) {
            var newStudent = new StudentModel();
            newStudent.mapFrom(self.studentForCreate());
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

    self.saveEditedStudent = function(student) {
        if (!self.studentForUpdate.isValid()) {
            return false;
        }
        var studentServerModel = self.studentForUpdate().toServerModel(self.group.id());
        studentProvider.put(studentServerModel, function() {
            student.mapFrom(self.studentForUpdate());
            student.mode(self.mods.display);
        });
    }

    self.addSubject = function () {
        closeAllEditing();
        $('#createSubject').openModal();
        var newSubject = new SubjectModel();
        self.subjectForCreate().mapFrom(newSubject);
        self.subjectForCreate().mode(self.mods.create);
        ko.validation.group(self.subjectForCreate); //TODO: ???
    };

    self.saveAddedSubject = function () {
        if (!self.subjectForCreate.isValid()) {
            return false;
        }
         $('#createSubject').closeModal();
        self.subjectForCreate().groupId(self.group.id());
        var subjectServerModel = self.subjectForCreate().toServerModel();
        subjectProvider.post(subjectServerModel, function (data) {
            var newSubject = new SubjectModel();
            newSubject.mapFrom(self.subjectForCreate());
            newSubject.id(data.Id);
           
            closeAllEditing();
            self.subjects.splice(0, 0, newSubject);
            self.redirectToMarksSubject(newSubject.id);
        });
       
    };

    self.editingSubject = new SubjectModel();

    self.editSubject = function (subject) {
        $('#editSubject').openModal();
        self.subjectForUpdate().mapFrom(subject);
        self.subjectForUpdate.valueHasMutated();
        subject.mode(self.mods.edit);
        self.editingSubject.mapFrom(subject);
    };

    self.saveEditedSubject = function (subject) {
        if (!self.subjectForUpdate.isValid()) {
            return false;
        }
        var subjectServerModel = self.subjectForUpdate().toServerModel();
        subjectServerModel.GroupId = self.group.id();
        subjectProvider.put(subjectServerModel, function () {
                self.subjects().forEach(function (element) {
                    if (self.subjectForUpdate().id() == element.id()) {
                        self.subjectForUpdate().mode(self.mods.display);
                        element.mapFrom(self.subjectForUpdate());
                    }
                })
            $('#editSubject').closeModal();
        });
       
    };
    self.redirectToMarksSubject = function(subjectId) { //TODO: refactor to redirect
        var url = "/group/" + self.group.id() + "/subject/" + subjectId();
        location = url;
    }

    self.init = function() {
        categoryProvider.get(function(data) {
            $(data).each(function(index, element) {
                var mappedItem = new CategoryModel(element);
                self.categories.push(mappedItem);
            });
            self.addStudent();
        });

        studentProvider.get(function(data) {
            $(data).each(function(index, element) {
                var mappedStudent = new StudentModel(element, self.mods.display);
                mappedStudent.sortMarksByCategoryIdDesc();
                self.students.push(mappedStudent);
            });
        });

        subjectProvider.getAll(function (data) {
            $(data).each(function (index, element) {
                var mappedSubject = new SubjectModel(element, self.mods.display);
                self.subjects.push(mappedSubject);
            });
        });
        self.sortingByName(sortingBy);
        self.preloader(false);
    }

    self.init();

    function closeAllEditing() {
        for (var k in self.students()) {
            self.students()[k].mode(self.mods.display);
        }
        self.studentForUpdate().mode(self.mods.display);
    }
}