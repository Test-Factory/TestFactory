//NEED REFACTOR
$(document).ready(function () {
    var create = $("#createGroup");

    $(".create").on("click", function () {
        if (create.css("display", "none")) {
            create.show();
    }
    else
            create.hide();
    })

    var update = $("#updateGroup");

    $(".edit").on("click", function () {
        if (update.css("display", "none")) {
            update.show();
        }
        else
            update.hide();
    })
});