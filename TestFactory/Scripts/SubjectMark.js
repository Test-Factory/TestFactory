SubmitForm = function (elem, path) {
    $(elem).parents(".form-mark").find(".preloader-visibility").show();
    $(elem).parent(".form-mark").find(".icon-mark").hide();
    var toServer = $(elem).parents(".form-mark").serialize();
    $.post(
        path,
        toServer,
        function (data) {
            $(elem).parents(".form-mark").find(".preloader-visibility").hide();
            $(elem).parent(".form-mark").find(".icon-mark").show();
        }
    )
}