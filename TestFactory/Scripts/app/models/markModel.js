function MarkModel(item) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.studentId = ko.observable(item ? item.StudentId : "");
    self.categoryId = ko.observable(item ? item.CategoryId : "");
    self.value = ko.observable(item ? item.Value : "").extend({ required: true }).extend({ number: true });
}
var validationOptions =
      { insertMessages: true, decorateElement: true, errorElementClass: 'errorFill' };
    ko.validation.init(validationOptions);