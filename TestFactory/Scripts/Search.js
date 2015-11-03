var sett = {}
   sett.ItemId = 0;

var getSearchResult = function () {
    if ($("#search").val() == '') $(".card-search-container").show(0);

    clearTimeout(sett.ItemId);
    $(this).removeClass('success', 100);
    sett.ItemId = setTimeout(function () {
        var value = getSearchResult.val();
        if (!value || !value.trim()) {
            $('#search').addClass('success');
            $(".cardWrapper").show(0);
            return;
        }
        $.ajax(
            {
                url: "/searchForStudents",
                type: "POST",
                data: { name: value },
                success: function (result) {
                    $(".card-search-container").hide(0);
                    for (var i = 0; i < result.length; i++) {
                        $("#" + result[i]).show(0);
                    }
                }
            });
    }, 500);
}
$(document).ready(function () {
    getSearchResult = getSearchResult = $("#search");
});

$("#search-clear").click(function () {
    $("#search").val("");
    $(".card-search-container").show(0);
});
$("#search").keyup(getSearchResult);

Delete = function (group, id) {
    $("#dialog").dialog({
        resizable: false,
        height: 200,
        modal: true,
        buttons: {
            "Видалити": function () {
                $.ajax({
                    method: "POST",
                    url: settings.basePath + "/studentDelete/" + id,
                    data: JSON.stringify(id),
                    success: function (msg) {
                        if (msg) group.parent().parent().parent().parent().remove();
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

Download = function (id) {
    $.ajax({
        method: "POST",
        url: settings.basePath + "/group/efgerg/" + id,
        data: JSON.stringify(id)
    })
}