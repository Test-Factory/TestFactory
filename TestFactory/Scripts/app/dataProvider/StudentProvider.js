function  StudentProvider() {
    var self = this;
    var path = settings.basePath;

    self.get = function(callback) {
        $.get(path).done(callback);
    }

    self.post = function(callback) {
        $.post(path).done(callback);
    }

    self.put = function(callback){
        $.put(path).done(callback);
    }
}