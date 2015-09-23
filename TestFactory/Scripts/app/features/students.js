function StudentsViewModel() {
    var self = this;
    var sp = new StudentProvider();
    self.students = ko.observableArray();
    self.mods = {
        display: "display",
        edit: "edit",
        create:"create"
    };

    self.addStudent = function() {
        console.log("add student");
    };

    sp.get(function (data) {
        $(data).each(function(index, element) {
            var mappedItem = new StudentModel(element, "display");
            debugger;
            self.students.push(mappedItem);
        });
    });

}