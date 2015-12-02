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


    self.init = function () {
        facultyProvider.get(function (data) {
            $(data).each(function (index, element) {
                var mappedFaculty = new FacultyModel(element, self.mods.display);
                self.students.push(mappedFaculty);

            });
            self.preloader(false);
        });
    }

    self.init();


}