Index = (function () {
    var localStrings = {};

    var createGroupPopUp = function () {
        $("#addGroup").on("click", function () {
            $("#createGroup").show();
        })
        $(".declineCreate").on("click", function () {
            $("#createGroup").hide();
        })
    }();

    var updateGroupPopUp = function () {
        $(".groupUpdate").on("click", function () {
            var $groupContainer = $(this).parents('.group-container').first();
            $('#updateGroup', $groupContainer).show();
        })

        $(".declineUpdate").on("click", function () {
            var $groupContainer = $(this).parents('.group-container').first();
            $('#updateGroup', $groupContainer).hide();
        })
    }();

    Delete = function (group, id) {
        $("#delete-group").dialog({       
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
                                if (msg === true)
                                    group.parent().parent().parent().parent().parent().remove();
                                else if (msg === false)
                                    location = "login";
                                else
                                    location = "/Error/403";

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
   
})();
