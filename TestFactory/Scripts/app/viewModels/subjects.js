var bindingHandlersManager = {
    addTooltips: function() {
        ko.bindingHandlers.tooltip = {
            update: function(element, valueAccessor) {
                var options = valueAccessor() || {};
                ko.utils.unwrapObservable(valueAccessor());
                if (options.message()) {
                    $(element).tooltip("remove");
                    $(element).attr('data-tooltip', options.message());
                    $(element).tooltip({ delay: 0 });
                } else {
                    $(element).tooltip("remove");
                }
            }
        };
    },
   
    //addTypeahead: function() {
    //    ko.bindingHandlers['typeahead'] = {
    //        init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
    //            var options = valueAccessor() || {};
    //            // call bootstrap type ahead
    //            var baseOptions = {
    //                hint: true,
    //                highlight: true,
    //                minLength: 1
    //            };
    //            $(element).typeahead(baseOptions, options);
    //            // typeahed on select update observable
    //            $(element).bind('typeahead:selected', function (obj, data, name) {
    //                allBindings().value(data);
    //            });
    //        }
    //    };
    //}
}

bindingHandlersManager.addTooltips();
//bindingHandlersManager.addTypeahead();

function SubjectViewModel(group, subject) {
    var self = this;
    self.subject = new SubjectModel(subject);
    self.subjectsInGroup = new SubjectsInGroupModel(group);
    self.subjects = ko.observableArray();
    var subjectProvider = new SubjectProvider(self.subjectsInGroup.id);
    subjectProvider.getAll(function (data) {
        $(data).each(function (index, element) {
            var mappedSubject = new SubjectModel(element, self.mods.display);
            self.subjects.push(mappedSubject);
        });
    });

    self.redirectToMarksSubject = function (subjectId) {
        var url = "/group/" + self.subjectsInGroup.id() + "/subject/" + subjectId();
        location = url;
    }
    self.chechedSubject = ko.observable(false);
    self.boldSubjectSadebar = ko.pureComputed(function () {
        return self.chechedSubject() ? "boldSubjectSadebar" : "";
    }, self);
    self.addSubject = function () {
        closeAllEditing();
        var newSubject = new SubjectWithGroupModel();
        self.subjectForCreate().mapFrom(newSubject);
        self.subjectForCreate().mode(self.mods.create);

        ko.validation.group(self.subjectForCreate); //TODO: ???
    };

    self.saveAddedSubject = function () {
        if (!self.subjectForCreate.isValid()) {
            return false;
        }
        self.subjectForCreate().groupId(self.subjectsInGroup.id());
        var subjectServerModel = self.subjectForCreate().toServerModel();
        subjectProvider.post(subjectServerModel, function (data) {
            var newSubject = new SubjectWithGroupModel();
            newSubject.mapFrom(self.subjectForCreate());
            newSubject.subjectId(data.Id);
            closeAllEditing();
            self.subjects.splice(0, 0, newSubject);
            self.addSubject();
        });
    };


    

    self.availableSubjects = self.subjectsInGroup.subjects;
    self.sortDescending = ko.observable(false);
    self.sortingKey = ko.observable("name");

  

    self.searchSubjects = function (searchTerm, callback, asyncCallback) {
        subjectProvider.getAll(function (arr) {
            asyncCallback(arr);
        });
    }

    self.preloader = ko.observable(true);
    self.subjectForUpdate = ko.validatedObservable(new SubjectWithGroupModel(), { deep: true });
    self.subjectForCreate = ko.validatedObservable(new SubjectWithGroupModel(), { deep: true });
    self.subjecttForDelete = ko.validatedObservable(new SubjectWithGroupModel(), { deep: true });

    self.mods = {
        display: "display",
        edit: "edit",
        deleting: "delete"
    };

    self.sortingByName = function() {
        if (self.sortDescending()) {
            self.subjectsInGroup.subjects.sort(function(left, right) {
                return compareByKeyDesc(left, right, self.sortingKey());
            });
            self.sortDescending(false);
        } else {
            self.subjectsInGroup.subjects.sort(function(left, right) {
                return compareByKeyAsc(left, right, self.sortingKey());
            });
            self.sortDescending(true);
        }
    };
    self.selectedClassForSortedField = ko.pureComputed(function() {
        return self.sortDescending() ? "triangle-up" : "triangle-down";
    }, self);

    self.deleteSubject = function (subject) {
        $("#delete-student").dialog({
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "Видалити": function () {
                    closeAllEditing();
                    self.subjecttForDelete().mapFrom(subject);
                    //self.categories.removeAll();
                    //self.students.remove(student);
                    subject.mode(self.mods.deleting);
                    var subjectServerModel = self.subjecttForDelete().toServerModel();
                    subjectServerModel.GroupId = self.subjectsInGroup.id();
                    subjectProvider.delete(subjectServerModel, function () {
                        subject.mapFrom(self.subjecttForDelete());
                        subject.mode(self.mods.deleting);
                    });
                    $(this).dialog("close");
                },
                Відмінити: function () {
                    $(this).dialog("close");
                }
            }
        });
    }


    self.editSubject = function(subject) {
        closeAllEditing();
        self.subjectForUpdate().mapFrom(subject);
        self.subjectForUpdate.valueHasMutated();

        subject.mode(self.mods.edit);
    };

    

    self.saveEditedSubject = function(subject) {
        if (!self.subjectForUpdate.isValid()) {
            return false;
        }
        var subjectServerModel = self.subjectForUpdate().toServerModel();
        subjectServerModel.GroupId = self.subjectsInGroup.id();
        subjectProvider.put(subjectServerModel, function() {
            subject.mapFrom(self.subjectForUpdate());
            subject.mode(self.mods.display);
        });
    };
    self.init = function() {
        self.sortingByName();
    };
    self.init();

    function closeAllEditing() {
        for (var k in self.subjectsInGroup.subjects()) {
            self.subjectsInGroup.subjects()[k].mode(self.mods.display);
        }
        self.subjectForUpdate().mode(self.mods.display);
    }

};