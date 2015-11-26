
SubmitForm = function (elem, path) {
    $(elem).parents(".form-mark").find(".preloader-visibility").show();
    $(elem).parent(".form-mark").find(".icon-mark").hide();
    var toServer = $(elem).parents(".form-mark").serialize();
    $.post(
        path,
        toServer,
        function (data) {
            if (data) {
                Materialize.toast('Успішно відредаговано', 4000)
                $(elem).parents(".form-mark").find(".preloader-visibility").hide();
                $(elem).parent(".form-mark").find(".icon-mark-ok").show();
                setTimeout(function () { $(elem).parent(".form-mark").find(".icon-mark-ok").hide(); }, 4000);
                $(elem).removeClass("valid");
                $(elem).removeClass("input-validation-error");
            }
            else {
                Materialize.toast('Дані некоректні!', 4000)
                $(elem).parents(".form-mark").find(".preloader-visibility").hide();
                $(elem).parent(".form-mark").find(".icon-mark-bad").show();
                alert(this.timer.Item);
                setTimeout(function () { $(elem).parent(".form-mark").find(".icon-mark-bad").hide(); }, 4000);
                $(elem).removeClass("valid");
                $(elem).addClass("input-validation-error")
                setTimeout(function () {$(elem).removeClass("input-validation-error")}, 4000);
            }
        }
    )
}