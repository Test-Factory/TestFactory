function RoleProvider() {
    var self = this;
    self.apiPath = settings.basePath + "/api/roles";
}

$(function () {
    var contentType = "application/json; charset=utf-8";

    RoleProvider.prototype.get = function (callback) {
        $.get(this.apiPath).done(callback).error(function () { console.log("error"); });
    }

    RoleProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }

    RoleProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }
});
