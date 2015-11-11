function StudentModel(item, defaultMode) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.firstName = ko.observable(item ? item.FirstName : "").extend({
        required: {
        params: true,
        message: "Це поле є обов'язковим"
    }
    }).extend({
        maxLength: {
            params: 70,
            message: "Перевищує ліміт в 70 символів."
        }
    });
    self.lastName = ko.observable(item ? item.LastName : "").extend({
        required: {
            params: true,
            message: "Це поле є обов'язковим"
        }
    }).extend({
        maxLength: {
            params: 70,
            message: "Перевищує ліміт в 70 символів."
        }
    });
    self.groupId = ko.observable(item ? item.GroupId : "");
    self.mode = ko.observable(defaultMode || "display");

    self.marks = ko.observableArray();

    if (item && item.Marks) {
        item.Marks.forEach(function(element) {
            var mappedItem = new MarkModel(element);
            self.marks.push(mappedItem);
        });
    }
}
StudentModel.prototype.mapFrom = function mapStudent(from) {
    this.id(from.id());
    this.firstName(from.firstName());
    this.lastName(from.lastName());
    this.groupId(from.groupId());
    this.marks.removeAll();
    for (var m in from.marks()) {
        this.marks.push(from.marks()[m]);
    }
}

