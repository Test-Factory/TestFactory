//function SubjectWithGroupModel(item, defaultMode) {
//    var self = this;
//    self.subjectId = ko.observable(item ? item.SubjectId : "");
//    self.groupId = ko.observable(item ? item.GroupId : "");
//    self.name = ko.observable(item ? item.Name : "").extend({
//        required: {
//            params: true,
//            message: "Це поле є обов'язковим"
//        }
//    }).extend({
//        maxLength: {
//            params: 100,
//            message: "Перевищує ліміт в 100 символів."
//        }
//    });
//    self.mode = ko.observable(defaultMode || "display");
//}
//SubjectWithGroupModel.prototype.mapFrom = function mapSubject(from) {
//    this.subjectId(from.subjectId ? from.subjectId() : from.id());
//    this.groupId(from.groupId());
//    this.name(from.name());
//}

//SubjectWithGroupModel.prototype.toServerModel = function () {
//    return {
//        SubjectId: this.subjectId(),
//        GroupId: this.groupId(),
//        Name: this.name()
//    }
//}