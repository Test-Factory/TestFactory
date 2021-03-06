﻿

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

    self.sizeTable = function () {
        $("thead tr:nth-child(1) th").each(function (index, element) {
            $("thead tr:nth-child(1) th:nth-child(" + (index + 1) + ")").width($("tbody tr:nth-child(2) td:nth-child(" + (index + 1) + ")").width());
        });
    }

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

    self.addSubject = function () {
        closeAllEditing();
        $('#createSubject').openModal();
        var newSubject = new SubjectModel();
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
        });
      
    };

    self.editingSubject = new SubjectModel();

    self.editSubject = function(subject) {
        closeAllEditing();
        $('#editSubject').openModal();
        self.subjectForUpdate().mapFrom(subject);
        self.subjectForUpdate.valueHasMutated();
        subject.mode(self.mods.edit);
        self.editingSubject.mapFrom(subject);
    };

    self.closeEditSubject = function () {
        self.subjects().forEach(function (element) {
            if (self.editingSubject.id() == element.id()) {
                self.editingSubject.mode(self.mods.display);
                element.mapFrom(self.editingSubject);
            }
        })
        $('#editSubject').closeModal();
    }

    self.saveEditedSubject = function(subject) {
        if (!self.subjectForUpdate.isValid()) {
            return false;
        }
        var subjectServerModel = self.subjectForUpdate().toServerModel();
        subjectServerModel.GroupId = self.subjectsInGroup.id();
       
        subjectProvider.put(subjectServerModel, function () {
            if (self.subject.id() == self.subjectForUpdate().id()) {
                self.subject.longName(self.subjectForUpdate().longName());
            } 
                self.subjects().forEach(function (element)
                {
                    if (self.subjectForUpdate().id() == element.id()) {
                        self.subjectForUpdate().mode(self.mods.display);
                        element.mapFrom(self.subjectForUpdate());
                    }     
                })
            
            $('#editSubject').closeModal();
        });
        
    };

    self.sizeTable();

    function closeAllEditing() {
        for (var k in self.subjectsInGroup.subjects()) {
            self.subjectsInGroup.subjects()[k].mode(self.mods.display);
        }
        self.subjectForUpdate().mode(self.mods.display);
    }
};