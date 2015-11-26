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
}

bindingHandlersManager.addTooltips();

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
            self.subjects.push(mappedSubject); 
        });
    });

    self.redirectToMarksSubject = function (subjectId) {
        var url = "/group/" + self.subjectsInGroup.id() + "/subject/" + subjectId();
        location = url;
    }

    self.sortingByName = function () {
        self.subjects.sort(function (left, right) {
            return compareByKeyAsc(left, right, "shortName");
        });
    };


    self.addSubject = function () {
        //closeAllEditing();
        $('#createSubject').openModal();
        var newSubject = new SubjectpModel();
        self.subjectForCreate().mapFrom(newSubject);
        self.subjectForCreate().mode(self.mods.create);
        ko.validation.group(self.subjectForCreate); 
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
            $('#createSubject').closeModal();
            self.addSubject();
        });
      
    };

    self.editingSubject = new SubjectModel();

    self.editSubject = function(subject) {
        $('#editSubject').openModal();
        self.subjectForUpdate().mapFrom(subject);
        self.subjectForUpdate.valueHasMutated();
        subject.mode(self.mods.edit);
        self.editingSubject.mapFrom(subject);
    }; 

    self.saveEditedSubject = function(subject) {
        if (!self.subjectForUpdate.isValid()) {
            return false;
        }
        var subjectServerModel = self.subjectForUpdate().toServerModel();
        subjectServerModel.GroupId = self.subjectsInGroup.id();
       
        subjectProvider.put(subjectServerModel, function () {
            if (self.subject.id() == self.subjectForUpdate().id()) {
                self.redirectToMarksSubject(self.subject.id);
            } else {
                self.subjects().forEach(function (element)
                {
                    if (self.subjectForUpdate().id() == element.id()) {
                        self.subjectForUpdate().mode = self.mods.display;
                        element.mapFrom(self.subjectForUpdate());
                    }     
                })
            }
            $('#editSubject').closeModal();
        });
        };

    function closeAllEditing() {
        for (var k in self.subjectsInGroup.subjects()) {
            self.subjectsInGroup.subjects()[k].mode(self.mods.display);
        }
        self.subjectForUpdate().mode(self.mods.display);
    }
};