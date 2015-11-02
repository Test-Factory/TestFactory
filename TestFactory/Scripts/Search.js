var ItemId = 0;

var getSearchResult = function () {
    if ($("#search").val()=='') $(".card-search-container").show(0);
    clearTimeout(ItemId);
    $(this).removeClass('success', 100);
    ItemId = setTimeout(function () {
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