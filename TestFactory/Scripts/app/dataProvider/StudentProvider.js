function StudentProvider(groupId) {
    var self = this;
    self.apiPath = settings.basePath + "/api/students";
    if (groupId) {
        self.groupId = groupId();
    }
}

$(function () {
    var contentType = "application/json; charset=utf-8";
    StudentProvider.prototype.get = function (callback) {
        $.getJSON(this.apiPath, { groupId: this.groupId }).done(callback).error(function () { console.log("error"); });
    }

    
    StudentProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback).error(function () { location = "/Error/403" });;
    }

    StudentProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: this.apiPath,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback).error(function () { location = "/Error/403" });;
    }

    StudentProvider.prototype.delete= function (data,callback) {
        $.ajax({
            method: "POST",
            url: this.apiPath + "/delete",
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback).error(function () { location = "/Error/403" });
    }
});

  
