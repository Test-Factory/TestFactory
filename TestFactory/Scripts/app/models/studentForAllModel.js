function StudentForAllModel(item, averageMarks) {
    var self = this;

    self.id = ko.observable(item ? item.Id : "");
    self.firstName = ko.observable(item ? item.FirstName : "");
    self.lastName = ko.observable(item ? item.LastName : "");
    self.groupId = ko.observable(item ? item.GroupId : "");
    self.groupShortName = ko.observable(item ? item.ShortName : "");
    self.year = ko.observable(item ? item.Year : "");

    self.marks = ko.observableArray();
    if (item && item.Marks) {
        var index = 0;
        item.Marks.forEach(function (element) {
            var mappedItem = new MarkModel(element, averageMarks[index]);
            self.marks.push(mappedItem);
            index++;
        });
    }
}
StudentForAllModel.prototype.sortMarksByCategoryIdDesc = function () {
    this.marks.sort(function (leftMark, rightMark) {
        return compareStudentMarksByCategoryIdDesc(leftMark, rightMark);
    });
}