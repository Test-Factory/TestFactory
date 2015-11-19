function GroupProvider() {
    var self = this;
    self.apiPath = settings.basePath + "/api/groups";
}

$(function () {
    var contentType = "application/json; charset=utf-8";

    GroupProvider.prototype.get = function (id, callback) {
        $.getJSON(this.apiPath, { groupId: id })
            .done(callback)
            .error(function(e) {
                 console.log(e);
        });
    }

    GroupProvider.prototype.getNames = function (path, callback) {
        $.getJSON(this.apiPath + path)
            .done(callback)
            .error(function (e) {
                console.log(e);
            });
    }

    GroupProvider.prototype.getYears = function (path, callback) {
        $.getJSON(this.apiPath + path)
            .done(callback)
            .error(function (e) {
                console.log(e);
            });
    }

    GroupProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: path,
            data: JSON.stringify(data).error(function () { location = "/Error/403" }),
            contentType: contentType,
        }).done(callback).error(function () { location = "/Error/403" });;
    }

    GroupProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback).error(function () { location = "/Error/403" });;
    }
});