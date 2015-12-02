function FacultyProvider(groupId) {
    var self = this;
    self.apiPath = settings.basePath + "/api/faculties";
}

$(function () {
    var contentType = "application/json; charset=utf-8";

    FacultyProvider.prototype.get = function (callback) {
        $.get(path).done(callback).error(function () { console.log("error"); });
    }

    FacultyProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }

    FacultyProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }
});


