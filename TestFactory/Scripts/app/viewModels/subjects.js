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
   
    addTypeahead: function() {
        ko.bindingHandlers['typeahead'] = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                var options = valueAccessor() || {};
                // call bootstrap type ahead
                var baseOptions = {
                    hint: true,
                    highlight: true,
                    minLength: 1
                };
                $(element).typeahead(baseOptions, options);

                // typeahed on select update observable
                $(element).bind('typeahead:selected', function (obj, data, name) {
                    allBindings().value(data);
                });
            }
        };
    }
}

bindingHandlersManager.addTooltips();
bindingHandlersManager.addTypeahead();

function SubjectViewModel(group) {
    var self = this;

    self.subjectsInGroup = new SubjectsInGroupModel(group);

    self.availableSubjects = self.subjectsInGroup.subjects;
    self.sortDescending = ko.observable(false);
    self.sortingKey = ko.observable("name");

    var subjectProvider = new SubjectProvider(self.subjectsInGroup.id);

    self.searchSubjects = function (searchTerm, callback, asyncCallback) {
        subjectProvider.getAll(function (arr) {
            asyncCallback(arr);
        });
    }

    self.preloader = ko.observable(true);
    self.subjectForUpdate = ko.validatedObservable(new SubjectModel(), { deep: true });
    self.subjectForCreate = ko.validatedObservable(new SubjectModel(), { deep: true });

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

    self.editSubject = function(subject) {
        closeAllEditing();
        self.subjectForUpdate().mapFrom(subject);
        self.subjectForUpdate.valueHasMutated();

        subject.mode(self.mods.edit);
    };

    self.addSubject = function() {
        closeAllEditing();
        var newSubject = new SubjectModel();
        self.subjectForCreate().mapFrom(newSubject);
        self.subjectForCreate().mode(self.mods.create);

        ko.validation.group(self.subjectForCreate); //TODO: ???
    };

    self.saveAddedSubject = function() {
        if (!self.subjectForCreate.isValid()) {
            return false;
        }
        var subjectServerModel = self.subjectForCreate().toServerModel();
        subjectProvider.post(subjectServerModel, function(data) {
            var newSubject = new SubjectModel();
            newSubject.mapFrom(self.subjectForCreate());
            newSubject.id(data.Id);
            closeAllEditing();
            self.subjectsInGroup.subjects.splice(0, 0, newSubject);
            self.addSubject();
        });
    };

    self.saveEditedSubject = function(subject) {
        if (!self.subjectForUpdate.isValid()) {
            return false;
        }
        var subjectServerModel = self.subjectForUpdate().toServerModel();
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