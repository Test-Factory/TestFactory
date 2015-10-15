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

function StudentsViewModel(group) {

    var self = this;
    self.group = new GroupModel(group);

    var studentProvider = new StudentProvider(self.group.id);
    var categoryProvider = new CategoryProvider();
    
    self.students = ko.observableArray();
    self.categories = ko.observableArray();
    
    self.studentForUpdate = ko.validatedObservable(new StudentModel(), { deep: true });
    self.studentForCreate = ko.validatedObservable(new StudentModel(), { deep: true });

    self.mods = {
        display: "display",
        edit: "edit",
        create:"create"
    };

    self.sortDescending = ko.observable(false);
    self.sortKey = ko.observable("lastName");

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
            self.students.sort(function(left, right) {
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

    self.addStudent = function () {
        closeAllEditing();
        mapStudent(new StudentModel(), self.studentForCreate());
        for (var c in self.categories()) {
            var mark = new MarkModel();
            mark.categoryId(self.categories()[c].id());
            self.studentForCreate().marks.push(mark);
        }
        self.studentForCreate().mode(self.mods.create);

        //self.studentForCreate().lastName.isModified(false);
        ko.validation.group(self.studentForCreate);
    };

    self.saveAddedStudent = function () {
        //ko.validation.group(self.studentForCreate(), {deep:true});
        self.studentForCreate().lastName.isModified(true);
        if (!self.studentForCreate.isValid()) {
            return false;
        }
        var studentServerModel = toServerStudentModel(self.studentForCreate());
        studentProvider.post(studentServerModel, function (data) {
            var newStudent = new StudentModel();
            mapStudent(self.studentForCreate(), newStudent);
            newStudent.id(data.Id);
            for(i in newStudent.marks()) {
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
    function comparedDescending(obj1, obj2, key) {
        if (obj1[key]().toUpperCase() == obj2[key]().toUpperCase())
            return 0;
        else if (obj1[key]().toUpperCase() < obj2[key]().toUpperCase())
            return 1;
        else
            return -1;
    }
    function compared(obj1, obj2, key) {
        if (obj1[key]().toUpperCase() == obj2[key]().toUpperCase())
            return 0;
        else if (obj1[key]().toUpperCase() < obj2[key]().toUpperCase())
            return -1;
        else
            return 1;
    }
}


