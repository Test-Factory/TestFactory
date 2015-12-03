function UserModel(item, defaultMode) {
    var self = this;
    self.mode = ko.observable(defaultMode ? defaultMode : "display");
    self.id = ko.observable(item ? item.Id : "");
    self.email = ko.observable(item ? item.Email : "")
        .extend({ email: {
            params: true,
            message: "Неправильний email"
        } })
        .extend({ maxLength: 50 });
    self.password = ko.observable(item ? item.Password : "")
        .extend({ maxLength: 255 });
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