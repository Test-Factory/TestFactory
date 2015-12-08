function SubjectMarkModel(item) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.studentId = ko.observable(item ? item.StudentId : "");
    self.subjectId = ko.observable(item ? item.SubjectId : "");
    if (item.Value != null) self.value = ko.observable(item ? item.Value : "");
    else
        self.value = ko.observable("-");
}
SubjectMarkModel.prototype.toServerModel = function () {
    return {
        Id: this.id(),
        StudentId: this.studentId(),
        SubjectId: this.subjectId(),
        Value: this.value()
    }
}


