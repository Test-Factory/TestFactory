﻿
var timer = {}
timer.Item = 0;

SubmitForm = function (elem, path) {
    $(elem).parents(".form-mark").find(".preloader-visibility").show();
    $(elem).parent(".form-mark").find(".icon-mark").hide();
    var toServer = $(elem).parents(".form-mark").serialize();
    clearInterval(timer.Item)
    $.post(
        path,
        toServer,
        function (data) {
            if (data) {
                Materialize.toast('Успішно відредаговано', 4000)
                $(elem).parents(".form-mark").find(".preloader-visibility").hide();
                $(elem).parent(".form-mark").find(".icon-mark-ok").show();
                timer.Item = setTimeout(function () { $(elem).parent(".form-mark").find(".icon-mark-ok").hide(); }, 4000);
                $(elem).removeClass("valid");
            }
            else {
                Materialize.toast('Дані не коректні!', 4000)
                $(elem).parents(".form-mark").find(".preloader-visibility").hide();
                $(elem).parent(".form-mark").find(".icon-mark-bad").show();
                timer.Item = setTimeout(function () { $(elem).parent(".form-mark").find(".icon-mark-bad").hide(); }, 4000);
                $(elem).removeClass("valid");
                $(elem).addClass("input-validation-error");
            }
        }
    )
}