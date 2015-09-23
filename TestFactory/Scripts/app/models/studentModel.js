function StudentModel(item, defaultMode) {
    var self = this;
    self.id = ko.observable(item.Id);
    self.firstName = ko.observable(item.FirstName);
    self.lastName = ko.observable(item.LastName);
    self.groupId = ko.observable(item.GroupId);
    self.mode = ko.observable(defaultMode);
}