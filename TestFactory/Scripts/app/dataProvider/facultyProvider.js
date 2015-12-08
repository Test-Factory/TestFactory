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
        }).done(callback).done(function () { Materialize.toast("Факультет з користувачами успішно створені", 4000); }).error(function (e) {
            if (e.responseText.length == 21)
                Materialize.toast("Факультет з таким ім'ям уже існує", 4000);
            else if (e.responseText.length == 19)
                location = "/Error/403"
            else 
                Materialize.toast('Користувач з такою електронною поштою вже існує', 4000);
        });;
    }

    FacultyProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback).done(function () { Materialize.toast("Факультет з користувачами успішно відредаговані", 4000); }).error(function (e) {
            if (e.responseText.length == 21)
                Materialize.toast("Факультет з таким ім'ям уже існує", 4000);
            else if (e.responseText.length == 19)
                location = "/Error/403"
            else
                Materialize.toast('Користувач з такою електронною поштою вже існує', 4000);
        });;
    }
});


