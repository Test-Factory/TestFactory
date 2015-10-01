function GroupModel(item) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.fullName = ko.observable(item ? item.FullName : "");
    self.shortName = ko.observable(item ? item.ShortName : "");
}