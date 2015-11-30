function StudentForAllModel(item) {
    var self = this;

    self.id = ko.observable(item ? item.Id : "");
    self.firstName = ko.observable(item ? item.FirstName : "");
    self.lastName = ko.observable(item ? item.LastName : "");
    self.groupId = ko.observable(item ? item.GroupId : "");
    self.groupShortName = ko.observable(item ? item.ShortName : "");
    self.year = ko.observable(item ? item.Year : 2015);

    self.marks = ko.observableArray();
    if (item && item.Marks) {
        item.Marks.forEach(function (element) {
            var mappedItem = new MarkModel(element);
            self.marks.push(mappedItem);
        });
    }

    self.subjectMarks = ko.observableArray();
    if (item && item.SubjectMarks) {
        var index = 0;
        item.SubjectMarks.forEach(function (element) {
            var mappedItem = new SubjectMarkModel(element);
            self.subjectMarks.push(mappedItem);
            index++;
        })
    }
}
StudentForAllModel.prototype.sortMarksByCategoryIdDesc = function () {
    this.marks.sort(function (leftMark, rightMark) {
        return compareStudentMarksByCategoryIdDesc(leftMark, rightMark);
    });
}
