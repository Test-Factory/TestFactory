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
            }
            else {
                Materialize.toast('Дані не коректні!', 4000)
                $(elem).parents(".form-mark").find(".preloader-visibility").hide();
                $(elem).parent(".form-mark").find(".icon-mark-bad").show();
            }
        }
    )
}