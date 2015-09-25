function CategoryProvider() { }

$(function () {
    var path = settings.basePath + "/api/Category";
    var contentType = "application/json; charset=utf-8";
    CategoryProvider.prototype.get = function (callback) {
        $.get(path).done(callback).error(function () { console.log("error"); });
    }

    CategoryProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }

    CategoryProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }
});