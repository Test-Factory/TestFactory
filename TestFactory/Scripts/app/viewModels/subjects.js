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
    self.subjectForUpdate = ko.validatedObservable(new SubjectModel(), { deep: true });
    self.subjectForCreate = ko.validatedObservable(new SubjectModel(), { deep: true });
    self.mods = {
        display: "display",
        edit: "edit",
        deleting: "delete"
    };
    var subjectProvider = new SubjectProvider(self.subjectsInGroup.id);

    subjectProvider.getAll(function (data) {
        $(data).each(function (index, element) {
            var mappedSubject = new SubjectModel(element, self.mods.display);
            self.subjects.push(mappedSubject); //TODO: to init();
        });
    });

    self.redirectToMarksSubject = function (subjectId) {
        var url = "/group/" + self.subjectsInGroup.id() + "/subject/" + subjectId();
        location = url;
    }

    self.addSubject = function () {
        closeAllEditing();
        var newSubject = new SubjectpModel();
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
            var newSubject = new SubjectModel();
            newSubject.mapFrom(self.subjectForCreate());
            newSubject.id(data.Id);
            closeAllEditing();
            self.subjects.splice(0, 0, newSubject);
            self.redirectToMarksSubject(newSubject.id);
            self.addSubject();
        });
    };


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

    function closeAllEditing() {
        for (var k in self.subjectsInGroup.subjects()) {
            self.subjectsInGroup.subjects()[k].mode(self.mods.display);
        }
        self.subjectForUpdate().mode(self.mods.display);
    }

};