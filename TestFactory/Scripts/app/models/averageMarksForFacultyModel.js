function AverageMarksForFacultyModel(item) {
    var self = this;

    self.faculty = ko.observable(item ? item.Faculty : "");
    self.id = ko.observable(item ? item.Id : "");
    self.code = ko.observable(item ? item.Code : "");
    self.average = ko.observable(item ? item.Average : "");

}