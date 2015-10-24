$(document).ready(function () {

    $("#addGroup").on("click", function () {
        $("#createGroup").show();
    })
    $(".groupUpdate").on("click", function () {		
                var $groupContainer = $(this).parents('.group-container').first();		
                $('#updateGroup',$groupContainer).show();		
    })
    $(".decline").on("click", function () {
        $("#createGroup").hide();
        $("#updateGroup").hide();
    })

    Delete = function (group, id) {
        $.ajax({
            method: "POST",
            url: settings.basePath + "/group/delete/" + id,
            data: JSON.stringify(id),
        })
    }

    /*$(".card").mouseenter(function () {
        $(".deleteContainer").stop(true);
        $(".deleteContainer").show("slow");
    });

    $(".card").mouseleave(function () {
        $(".deleteContainer").stop(true);
        $(".deleteContainer").hide("slow");
    });*/


});