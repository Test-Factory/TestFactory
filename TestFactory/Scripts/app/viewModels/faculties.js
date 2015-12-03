function FacultiesViewModel() {
    var self = this;

    self.faculties = ko.observableArray();
    self.preloader = ko.observable(true);

    self.roles = ko.observableArray();//TODO: role provider
    self.roles.push("12dc6a23-8454-419f-ac75-2ea0560d27ef");
    self.roles.push("316987d9-9e4e-4cc4-b32a-b64112ca20be");
   

    self.facultyForUpdate = ko.validatedObservable(new FacultyModel(), { deep: true });
    self.facultyForCreate = ko.validatedObservable(new FacultyModel(), { deep: true });

    var facultyProvider = new FacultyProvider();

    self.mods = {
        display: "display",
        edit: "edit",
        deleting: "delete",
        create: "create"
    };

    self.addFaculty = function () {
        var newFaculty = new FacultyModel();
        self.facultyForCreate().mapFrom(newFaculty);
        for (var c = 0; c < 2; c++) {
            var user = new UserModel();
            user.roles_id(self.roles()[c]);
            self.facultyForCreate().users.push(user);
        }
        self.facultyForCreate().mode(self.mods.create);
        ko.validation.group(self.facultyForCreate);
       
    }

    self.saveAddedFaculty = function () {
        //if (!self.facultyForCreate.isValid()) {
        //    return false;
        //}
        var facultyServerModel = self.facultyForCreate().toServerModel();
        facultyProvider.post(facultyServerModel, function (data) {
            var newFaculty = new FacultyModel();
            newFaculty.mapFrom(self.facultyForCreate());
            newFaculty.id(data.Id);
            newFaculty.mode(self.mods.display);
            for (i in newFaculty.users()) {
                newFaculty.users()[i].facultyId(newFaculty.id());
                for (j in data.Users) {
                    if (newFaculty.users()[i].roles_id() == data.Users[j].Roles_Id) {
                        newFaculty.users()[i].id(data.Users[j].Id);
                        break;
                    }
                }
            }
            closeAllEditing();
            self.faculties.splice(0, 0, newFaculty);
            self.addFaculty();
        });
    };

    self.editFaculty = function (faculty) {
        closeAllEditing();
        self.facultyForUpdate().mapFrom(faculty);
        faculty.mode(self.mods.edit);
        self.facultyForUpdate.valueHasMutated();
    };

    self.saveEditedFaculty = function (faculty) {
        //if (!self.facultyForUpdate.isValid()) {
        //    return false;
        //}
        var facultyServerModel = self.facultyForUpdate().toServerModel();
        facultyProvider.put(facultyServerModel, function () {
            faculty.mapFrom(self.facultyForUpdate());
            faculty.mode(self.mods.display);
        });
    }

        self.init = function () {
            facultyProvider.get(function (data) {
                $(data).each(function (index, element) {
                    var mappedFaculty = new FacultyModel(element, self.mods.display);
                    self.faculties.push(mappedFaculty);

                });
                self.addFaculty();
                self.preloader(false);
            });
        }

        self.init();
        function closeAllEditing() {
            for (var k in self.faculties()) {
                self.faculties()[k].mode(self.mods.display);
            }
            self.facultyForUpdate().mode(self.mods.display);
        }

}