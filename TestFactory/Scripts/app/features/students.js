function StudentsViewModel() {
    var sp = new StudentProvider();
    var self = this;
    self.students = ko.observableArray();
    self.categories = ko.observableArray();
//TODO: get category
    //[
    //    new CategoryModel({Id:1, Name: "R"}),
    //    new CategoryModel({ Id: 2, Name: "I" }),
    //    new CategoryModel({ Id: 3, Name: "A" }),
    //    new CategoryModel({ Id: 4, Name: "S" }),
    //    new CategoryModel({ Id: 5, Name: "E" }),
    //    new CategoryModel({ Id: 6, Name: "C" })

    //]);

    self.studentForUpdate = new StudentModel();
    self.studentForCreate = new StudentModel();

    self.mods = {
        display: "display",
        edit: "edit",
        create:"create"
    };

    self.addStudent = function() {
        console.log("add student");
    };

    self.editStudent = function (student) {
        closeAllEditing();
        mapStudent(student, self.studentForUpdate);
        student.mode(self.mods.edit);
    };

    self.addStudent = function () {
        closeAllEditing();
        mapStudent(new StudentModel(), self.studentForCreate);
        for (var c in self.categories()) {
            var mark = new MarkModel();
            mark.categoryId(self.categories()[c].id());
            self.studentForCreate.marks.push(mark);
        }
        self.studentForCreate.mode(self.mods.create);
    };

    self.saveAddedStudent = function () {
        var studentServerModel = toServerStudentModel(self.studentForCreate);
        sp.post(studentServerModel, function (id) {
            var newStudent = new StudentModel();
            mapStudent(self.studentForCreate, newStudent);
            newStudent.id(id);
            closeAllEditing();
            self.students.push(newStudent);
        });
    };

    self.saveEditedStudent = function (student) {
        var studentServerModel = toServerStudentModel(self.studentForUpdate);
        
        sp.put(studentServerModel, function () {
            mapStudent(self.studentForUpdate, student);
            student.mode(self.mods.display);
        });
    }       

    self.init = function() {
        sp.get(function (data) {
            $(data).each(function (index, element) {
                var mappedItem = new StudentModel(element, self.mods.display);
                self.students.push(mappedItem);
            });
        });
        sp.get(function (data) {
            $(data).each(function (index, element) {
                var mappedItem = new CategoryModel(element);
                self.categories.push(mappedItem);
            });
        });
    }

    self.init();

    function closeAllEditing() {
        for (var k in self.students()) {
            self.students()[k].mode(self.mods.display);
        }
        self.studentForCreate.mode(self.mods.display);
    }

    function toServerStudentModel(student) {
        return {
            Id: student.id(),
            FirstName: student.firstName(),
            LastName: student.lastName(),
            GroupId: student.groupId(),
            Marks: ko.utils.arrayMap(student.marks(), toServerMarkModel)
        }
    }

    function toServerMarkModel(mark) {
        return {
            Id: mark.id(),
            StudentId: mark.studentId(),
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