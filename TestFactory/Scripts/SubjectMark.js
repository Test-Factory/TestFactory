
var timer = {}
timer.Item = 0;
timer.Item2 = 0;

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
                this.timer.Item = setTimeout(function () { $(elem).parent(".form-mark").find(".icon-mark-ok").hide(); }, 4000);
                $(elem).removeClass("valid");
                $(elem).removeClass("input-validation-error");
            }
            else {
                Materialize.toast('Дані некоректні!', 4000)
                $(elem).parents(".form-mark").find(".preloader-visibility").hide();
                $(elem).parent(".form-mark").find(".icon-mark-bad").show();
                this.timer.Item = setTimeout(function () { $(elem).parent(".form-mark").find(".icon-mark-bad").hide(); }, 4000);
                $(elem).removeClass("valid");
                $(elem).addClass("input-validation-error")
                this.timer.Item2 = setTimeout(function () {$(elem).removeClass("input-validation-error")}, 4000);
            }
        }
    )
}