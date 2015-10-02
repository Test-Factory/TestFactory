function StudentModel(item, defaultMode) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.firstName = ko.observable(item ? item.FirstName : "").extend({ 
        required: { message: 'Please supply your Name.' }
    });
    self.lastName = ko.observable(item ? item.LastName : "");
    self.groupId = ko.observable(item ? item.GroupId : "");
    self.mode = ko.observable(defaultMode || "display");

    self.marks = ko.observableArray();

    if (item && item.Marks) {
        item.Marks.forEach(function(element) {
            var mappedItem = new MarkModel(element);
            self.marks.push(mappedItem);
        });
    }
}