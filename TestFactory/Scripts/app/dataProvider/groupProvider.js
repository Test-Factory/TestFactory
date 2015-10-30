function GroupProvider() { }

$(function () {
    var path = settings.basePath + "/api/groups";
    var contentType = "application/json; charset=utf-8";

    GroupProvider.prototype.get = function (groupId, callback) {
        $.getJSON(this.apiPath, { groupId: groupId() }).done(callback).error(function () { console.log("error"); });
    }

    GroupProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }

    GroupProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }
});