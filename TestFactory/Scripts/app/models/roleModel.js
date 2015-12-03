function RoleModel(item) {
    var self = this;

    self.id = ko.observable(item ? item.Id : "");
    self.name = ko.observable(item ? item.Name : "")
        .extend({ maxLength: 30 })
        .extend({
            required: {
                params: true,
                message: "Це поле є обов'язковим"
            }
        });
}