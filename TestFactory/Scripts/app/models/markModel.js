﻿function MarkModel(item) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.studentId = ko.observable(item ? item.StudentId : "");
    self.categoryId = ko.observable(item ? item.CategoryId : "");

    //self.testDescription = new CategoryModel(item ? item.TestDescription : null);
    self.value = ko.observable(item ? item.Value : 0);
}