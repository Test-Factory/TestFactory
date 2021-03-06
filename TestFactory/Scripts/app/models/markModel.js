﻿function MarkModel(item) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.studentId = ko.observable( item ? item.StudentId : "");
    self.categoryId = ko.observable( item ? item.CategoryId : "");
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
    }
    })
    .extend({
        min: {
            params: 0,
            message: "Будь ласка, введіть значення більше або рівне 0."
        }
    }).extend({ step: {
        params: 1,
        message: "Будь ласка, введіть ціле число."
    }
    });
}
MarkModel.prototype.mapFrom = function mapMark(from) {
    this.id(from.id());
    this.value(from.value());
    this.studentId(from.studentId());
    this.categoryId(from.categoryId());
   
}
MarkModel.prototype.toServerModel = function () {
    return {
        Id: this.id(),
        StudentId: this.studentId(),
        CategoryId: this.categoryId(),
        Value: this.value()
    }
}
MarkModel.prototype.getValue = function () { return this.value();}

