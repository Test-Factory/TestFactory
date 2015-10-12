$(document).ready(function () {

    $("#addGroup").on("click", function () {
        $("#createGroup").show();
    })
    $(".group-update").on("click", function () {
       // var $groupContainer = $(this).parents('.group-container').first();
        $('.updateGroup').show();
    })
    $(".decline").on("click", function () {
        $("#createGroup").hide();
        $("#updateGroup").hide();
    })

});