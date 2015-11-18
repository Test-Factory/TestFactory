function SubjectProvider(groupId) {
    var self = this;
    self.apiPath = settings.basePath + "/api/subjects";
    if (groupId) {
        self.groupId = groupId;
    }
}

$(function () {
    var contentType = "application/json; charset=utf-8";
    SubjectProvider.prototype.get = function (path, callback) {
        $.getJSON(this.apiPath + path)
            .done(callback)
            .error(function () { console.log("error"); });
    }

    SubjectProvider.prototype.getAll = function (callback) {
        $.getJSON(this.apiPath + "/all")
            .done(callback)
            .error(function () { console.log("error"); });
    }

    SubjectProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }

    SubjectProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }

    SubjectProvider.prototype.delete = function (data, callback) {
        $.ajax({
            method: "DELETE",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }
});