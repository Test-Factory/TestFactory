function StudentModel(item, defaultMode) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.firstName = ko.observable(item ? item.FirstName : "");
    self.lastName = ko.observable(item ? item.LastName : "");
    self.groupId = ko.observable(item ? item.GroupId : "");
    self.mode = ko.observable(defaultMode || "display");
}