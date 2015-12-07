function FacultyModel(item, defaultMode) {
    var self = this;
    
    self.id = ko.observable(item ? item.Id : "");
    self.name = ko.observable(item ? item.Name : "")
        .extend({ maxLength: 50 })
        .extend({
            required: {
                params: true,
                message: "Це поле є обов'язковим"
            }
        });
    self.users = ko.observableArray();
    self.mode = ko.observable(defaultMode ? defaultMode : "display");
    if (item && item.Users) {
        item.Users.forEach(function (element) {
            var mappedItem = new UserModel(element);
            self.users.push(mappedItem);
        });
    }
}
FacultyModel.prototype.toServerModel = function () {

    var serverUsers = this.users();
    var userList = [];
    serverUsers.forEach(function (element) {
        userList.push(element.toServerModel());
    });
    return {
        Id: this.id(),
        Name: this.name(),
        Users: userList
    }
}
FacultyModel.prototype.mapFrom = function mapFaculty(from) {
    this.id(from.id());
    this.name(from.name());
    this.mode(from.mode() ? from.mode() : "display");
    this.users.removeAll();
    for (var u in from.users()) {
        var mappedUser = new UserModel();
        mappedUser.mapFrom(from.users()[u]);
        this.users.push(mappedUser);
    }
}