function SubjectModel(item, defaultMode) {
    var self = this;
    self.id = ko.observable(item ? item.Id : "");
    self.groupId = ko.observable(item ? item.GroupId : "");
    self.name = ko.observable(item ? item.Name : "").extend({
        required: {
            params: true,
            message: "Це поле є обов'язковим"
        }
    }).extend({
        maxLength: {
            params: 100,
            message: "Перевищує ліміт в 100 символів."
        }
    });
    self.mode = ko.observable(defaultMode || "display");
}
SubjectModel.prototype.mapFrom = function mapSubject(from) {
    this.id(from.id());
    this.groupId(from.groupId());
    this.name(from.name());
}

SubjectModel.prototype.toServerModel = function () {
    return {
        Id: this.id(),
        GroupId: this.groupId(),
        Name: this.name()
    }
}

