function CategoryModel(item) { 
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.name = ko.observable(item ? item.Name : "");
    self.code = ko.observable(item ? item.Code : "");
    self.shortDescription = ko.observable(item ? item.ShortDescription : "");
    self.longDescription = ko.observable(item ? item.LongDescription : "");
}