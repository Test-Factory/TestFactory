function GroupProvider() { }

$(function () {
    var path = settings.basePath + "/api/groups";
    var contentType = "application/json; charset=utf-8";
    StudentProvider.prototype.get = function (callback) {
        $.get(path).done(callback).error(function () { console.log("error"); });
    }

    StudentProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }

    StudentProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }
});