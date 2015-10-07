function StudentModel(item, defaultMode) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.firstName = ko.observable(item ? item.FirstName : "").extend({
        required: {
        params: true,
        message: "Це поле є обов'язковим"
    }
    });
    self.lastName = ko.observable(item ? item.LastName : "").extend({
        required: {
            params: true,
            message: "Це поле є обов'язковим"
        }
    });
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

