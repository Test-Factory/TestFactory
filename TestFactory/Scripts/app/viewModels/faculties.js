function FacultiesViewModel() {
    var self = this;

    self.faculties = ko.observableArray();
    self.preloader = ko.observable(true);
   

    self.facultyForUpdate = ko.validatedObservable(new FacultyModel(), { deep: true });
    self.facultyForCreate = ko.validatedObservable(new FacultyModel(), { deep: true });

    var facultyProvider = new FacultyProvider();

    self.mods = {
        display: "display",
        edit: "edit",
        deleting: "delete"
    };

    self.addFaculty = function () {
        closeAllEditing();
        //self.facultyForCreate = new FacultyModel();
    }

    self.saveAddedSFaculty = function () {
        //if (!self.facultyForCreate.isValid()) {
        //    return false;
        //}
        var facultyServerModel = self.facultyForCreate().toServerModel();
        facultyProvider.post(facultyServerModel, function (data) {
            var newFaculty = new FacultyModel();
            newFaculty.mapFrom(self.facultyForCreate());
            newFaculty.id(data.Id);
            for (i in newFaculty.users()) {
                newFaculty.users()[i].facultyId(newFaculty.id());
                for (j in data.Users) {
                    if (newFaculty.users()[i].role_id() == data.Marks[j].Role_Id) {
                        newFaculty.users()[i].id(data.Users[j].Id);
                        break;
                    }
                }
               // self.sizeTable();
            }
            //self.sizeTable();
            closeAllEditing();
            //self.students.splice(0, 0, newStudent);
            self.addFaculty();
        });
    };

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