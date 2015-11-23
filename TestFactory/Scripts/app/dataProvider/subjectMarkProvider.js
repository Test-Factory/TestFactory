function SubjectMarkProvider(groupId) {
    var self = this;
    self.apiPath = settings.basePath + "/api/subjectsMark";
    if (groupId) {
        self.groupId = groupId;
    }
}

$(function () {
    var contentType = "application/json; charset=utf-8";
    SubjectMarkProvider.prototype.getAll = function (callback) {
        $.getJSON(this.apiPath + "/subjectMarkAll")
            .done(callback)
            .error(function () { console.log("error"); });
    }
});