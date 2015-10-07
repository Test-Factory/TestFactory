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

    var sp = new StudentProvider(self.group.id);
    var cp = new CategoryProvider();
    var mp = new MarkProvider();
    
    self.students = ko.observableArray();
    self.categories = ko.observableArray();
    
    self.studentForUpdate = ko.validatedObservable(new StudentModel(), { deep: true });
    self.studentForCreate = ko.validatedObservable(new StudentModel(), { deep: true });

    self.mods = {
        display: "display",
        edit: "edit",
        create:"create"
    };

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
    };

    self.downloadReport = function (student) {
        var studentServerModel = toServerStudentModel(student);
        sp.loadReport(studentServerModel, function () {
            console.log("result");
        });
    }

    self.saveAddedStudent = function () {
        if (!self.studentForCreate.isValid()) {
            return false;
        }
        
        var studentServerModel = toServerStudentModel(self.studentForCreate());
        sp.post(studentServerModel, function (data) {
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
        sp.put(studentServerModel, function () {
            mapStudent(self.studentForUpdate(), student);
            student.mode(self.mods.display);
        });
    }       

    self.init = function () {
        cp.get(function (data) {
            $(data).each(function (index, element) {
                var mappedItem = new CategoryModel(element);
                self.categories.push(mappedItem);
            });
            self.addStudent();
        });
        
        sp.get(function (data) {
            $(data).each(function (index, element) {
                var mappedItem = new StudentModel(element, self.mods.display);
                self.students.push(mappedItem);
            });
        });
    }
   
    self.init();

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


