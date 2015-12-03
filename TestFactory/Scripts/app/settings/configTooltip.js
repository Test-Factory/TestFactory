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
                    if (width > 240) {
                        $(element).attr('data-tooltip', options.message());
                        $(element).tooltip({ delay: 0 });
                    }

                } else {
                    $(element).tooltip("remove");
                }
            }
        };
    },
}