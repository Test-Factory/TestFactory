function StudentProvider(GroupId) {
    self = this;
    self.groupId = GroupId;
}

$(function() {
    var path = settings.basePath + "/api/Student";
    var contentType = "application/json; charset=utf-8";
    StudentProvider.prototype.get = function (id,callback) {
        $.get(path, id).done(callback).error(function() { console.log("error") ;} );
    }
    
    StudentProvider.prototype.post = function (data, callback) {
        $.ajax({
            method: "POST",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }

    StudentProvider.prototype.put = function (data, callback) {
        $.ajax({
            method: "PUT",
            url: path,
            data: JSON.stringify(data),
            contentType: contentType,
        }).done(callback);
    }
});

  
