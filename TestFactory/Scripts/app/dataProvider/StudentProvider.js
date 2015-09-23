function StudentProvider() {}

$(function() {
    var path = settings.basePath + "/api/students";

    StudentProvider.prototype.get = function (callback) {
        $.get(path).done(callback);
    }

    StudentProvider.prototype.post = function (callback) {
        $.post(path).done(callback);
    }

    StudentProvider.prototype.put = function (callback) {
        $.put(path).done(callback);
    }
});

  
