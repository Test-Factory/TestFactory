function StudentProvider(groupId) {
    var self = this;
    self.apiPath = settings.basePath + "/api/students";
    self.groupId = groupId;
}

$(function () {
    var contentType = "application/json; charset=utf-8";
    StudentProvider.prototype.get = function (callback) {
        $.get(this.apiPath, {groupId: this.groupId}).done(callback).error(function () { console.log("error"); });
    }
    
    StudentProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }

    StudentProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }
});

  
