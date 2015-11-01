function StudentForAllModel(item) {
    var self = this;
    var groupProvider = new GroupProvider();

    self.id = ko.observable(item ? item.Id : "");
    self.firstName = ko.observable(item ? item.FirstName : "");
    self.lastName = ko.observable(item ? item.LastName : "");
    self.groupId = ko.observable(item ? item.GroupId : "");

    self.groupShortName = ko.observable();
    groupProvider.get(self.groupId(), function(data) {
        self.groupShortName(data.ShortName);
    });

    self.marks = ko.observableArray();
    if (item && item.Marks) {
        item.Marks.forEach(function (element) {
            var mappedItem = new MarkModel(element);
            self.marks.push(mappedItem);
        });
    }
}