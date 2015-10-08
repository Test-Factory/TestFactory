ko.validation.rules.pattern.message = 'Invalid.';

ko.validation.init({
    registerExtenders: true,
    messagesOnModified: false,
    insertMessages: false,
    parseInputAttributes: true,
    messageTemplate: null,
    decorateElement: true,
    //errorElementClass: 'invalid',
    grouping: { deep: true, observable: false },
    decorateInputElement: true
}, false);

