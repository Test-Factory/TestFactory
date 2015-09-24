function TestDescriptionModel(item) { //TODO: TestCategoryModel
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.category = ko.observable(item ? item.Category : "");//TODO: name
    self.code = ko.observable(item ? item.Code : "");
    self.shortDescription = ko.observable(item ? item.ShortDescription : "");
    self.longDescription = ko.observable(item ? item.LongDescription : "");
}