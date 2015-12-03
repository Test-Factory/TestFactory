function FacultyProvider() {
    var self = this;
    self.apiPath = settings.basePath + "/api/faculties";
}

$(function () {
    var contentType = "application/json; charset=utf-8";

    FacultyProvider.prototype.get = function (callback) {
        $.get(this.apiPath).done(callback).error(function () { console.log("error"); });
    }

    FacultyProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback).error(function (e) {
            if (e.responseText)
                location = "/login";
            else
                location = "/Error/403";
        });;
    }

    FacultyProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback).error(function (e) {
            if (e.responseText)
                location = "/login";
            else
                location = "/Error/403";
        });;
    }
});


