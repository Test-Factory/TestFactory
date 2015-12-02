function UserModel(item, defaultMode) {
    var self = this;
    self.mode = ko.observable(defaultMode ? defaultMode : "display");
    self.id = ko.observable(item ? item.Id : "");
    self.email = ko.observable(item ? item.Email : "");
    self.password = ko.observable(item ? item.Password : "");
    self.roles_id = ko.observable(item ? item.Roles_id : "");
    self.facultyId = ko.observable(item ? item.FacultyId : "");
}
UserModel.prototype.toServerModel = function () {
    return {
        Id: this.id(),
        Email: this.email(),
        Password: this.password(),
        Roles_id: this.roles_id(),
        FacultyId: this.facultyId()
    }
}