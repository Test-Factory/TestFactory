function StudentsViewModel() {
    var sp = new StudentProvider();
    var self = this;
    self.students = ko.observableArray();

    self.studentForUpdate = new StudentModel();

    self.mods = {
        display: "display",
        edit: "edit",
        create:"create"
    };

    self.addStudent = function() {
        console.log("add student");
    };

    self.editStudent = function (student) {
        for (var k in self.students) {
            self.students[k].mode(self.mods.display);
        }
        mapStudent(student, self.studentForUpdate);
        student.mode(self.mods.edit);
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
    }

    self.init();

    function toServerStudentModel(student) {
        return {
            Id:student.id(),
            FirstName: student.firstName(),
            LastName: student.lastName(),
            GroupId: student.groupId()
        }
    }

    function mapStudent(from, to) {
        to.id(from.id());
        to.firstName(from.firstName());
        to.lastName(from.lastName());
        to.groupId(from.groupId());
    }
}