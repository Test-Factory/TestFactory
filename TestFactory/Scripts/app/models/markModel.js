function MarkModel(item) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.studentId = ko.observable(item ? item.StudentId : "");
    self.categoryId = ko.observable(item ? item.CategoryId : "");
    self.value = ko.observable(item ? item.Value : "").extend({
        required: {
            params: true,
            message: "Це поле є обов'язковим"
        }
    }).extend({
        number:{
            params: true,
            message: "Будь ласка, введіть число."
        }
    }).extend({ max: {
        params: 100,
        message: "Будь ласка, введіть значення менше або рівне 100."
    }});
}
var validationOptions =
      { insertMessages: true, decorateElement: true, errorElementClass: 'errorFill' };
    ko.validation.init(validationOptions);