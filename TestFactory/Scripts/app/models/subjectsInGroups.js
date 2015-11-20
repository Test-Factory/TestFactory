function SubjectsInGroupModel(item, defaultMode) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.fullName = ko.observable(item ? item.FullName : "");

    self.subjects = ko.observableArray();

    if (item && item.Subjects) {
        item.Subjects.forEach(function (element) {
            var mappedItem = new SubjectModel(element);
            self.subjects.push(mappedItem);
        });
    }
}
SubjectsInGroupModel.prototype.mapFrom = function mapStudent(from) {
    this.id(from.id());
    this.fullName(from.fullName());
    this.subjects.removeAll();
    for (var subject in from.subjects()) {
        this.subjects.push(from.subjects()[subject]);
    }
}

