var bindingHandlersManager = {
    addTooltips: function () {
        ko.bindingHandlers.tooltip = {
            update: function (element, valueAccessor) {
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
        ko.bindingHandlers.tooltipOverflow = {
            update: function (element, valueAccessor) {
                var options = valueAccessor() || {};
                ko.utils.unwrapObservable(valueAccessor());
                if (options.message()) {
                    $(element).tooltip("remove");
                    var width = $('.fakeDiv').text($(element).text()).width();
                    $('.fakeDiv').empty();
                    if (width > 200) {
                        $(element).attr('data-tooltip', options.message());
                        $(element).tooltip({ delay: 0 });
                    }
                } else {
                    $(element).tooltip("remove");
                }
            }
        };

        ko.bindingHandlers.tooltipOverflowSubjectBold = {
            update: function (element, valueAccessor) {
                var options = valueAccessor() || {};
                ko.utils.unwrapObservable(valueAccessor());
                if (options.message()) {
                    $(element).tooltip("remove");
                    var width = $('.fakeDivSubject').text($(element).text()).css('font-weight', 'bold').width();
                    $('.fakeDivSubject').empty();
                    if (width > 164) {
                        $(element).attr('data-tooltip', options.message());
                        $(element).tooltip({ delay: 0 });
                    }
                } else {
                    $(element).tooltip("remove");
                }
            }
        };
        ko.bindingHandlers.tooltipOverflowSubject = {
            update: function (element, valueAccessor) {
                var options = valueAccessor() || {};
                ko.utils.unwrapObservable(valueAccessor());
                if (options.message()) {
                    $(element).tooltip("remove");
                    var width = $('.fakeDivSubject').text($(element).text()).width();
                    $('.fakeDivSubject').empty();
                    if (width > 160) {
                        $(element).attr('data-tooltip', options.message());
                        $(element).tooltip({ delay: 0 });
                    }
                } else {
                    $(element).tooltip("remove");
                }
            }
        };

    }
}