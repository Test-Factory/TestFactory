$(document).ready(function () {

    $("#addGroup").on("click", function () {
        $("#createGroup").show();
    })
    $(".updateGroup").on("click", function () {
        var $groupContainer = $(this).parents('.group-container').first();

        $('.updateGroup',$groupContainer).show();
    })
    $(".decline").on("click", function () {
        $("#createGroup").hide();
        $("#updateGroup").hide();
    })

});