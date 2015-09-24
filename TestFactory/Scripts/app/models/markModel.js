function MarkModel(item) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.studentId = ko.observable(item ? item.StudentId : "");
    //self.testDescription = ko.observable(item ? item.TestDescription : "");
    self.value = ko.observable(item ? item.Value : "");
}