function MarkProvider() {
    var self = this;
    self.apiPath = settings.basePath + "/api/marks";
}

$(function () {
    var contentType = "application/json; charset=utf-8";
    MarkProvider.prototype.get = function (path,callback) {
        $.get(this.apiPath + path)
            .done(callback)
            .error(function () { console.log("error"); });
    }

    MarkProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }

    MarkProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }
});