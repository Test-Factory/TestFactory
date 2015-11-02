$(document).ready(function () {

    $("#addGroup").on("click", function () {
        $("#createGroup").show();
    })
    $(".groupUpdate").on("click", function () {		
                var $groupContainer = $(this).parents('.group-container').first();		
                $('#updateGroup',$groupContainer).show();		
    })

    $(".declineCreate").on("click", function () {
        $("#createGroup").hide();
    })

    $(".declineUpdate").on("click", function () {
        var $groupContainer = $(this).parents('.group-container').first();
        $('#updateGroup', $groupContainer).hide();
    })

    Delete = function (group, id) {
            $("#dialog").dialog({
                resizable: false,
                height: 200,
                modal: true,
                buttons: {
                    "Видалити": function () {
                        $.ajax({
                            method: "POST",
                            url: settings.basePath + "/group/delete/" + id,
                            data: JSON.stringify(id),
                            success: function (msg) {
                                if (msg) group.parent().parent().parent().parent().parent().remove();
                            }
                        })
                        $(this).dialog("close");
                    },
                    "Відмінити": function () {
                        $(this).dialog("close");
                        return false;
                    }
                }
            });
    }
});