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
        var valid = value.split('');
        var success = true;
        for (var i = 0; i < valid.length; i++)
        {
            if(valid[i] == '>' || valid[i] == '<')success = false;
        }
        if (success) {
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
        }
        else {
            $(".card-search-container").hide(0);
        }
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
    $("#delete-group").dialog({
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

Download = function (id) {
    $.ajax({
        method: "POST",
        url: settings.basePath + "/group/efgerg/" + id,
        data: JSON.stringify(id)
    })
}

swapCard = function (card, thisCard) {
    if (thisCard == 'back') $(card).parents('.card-search-container').css('border', '1px solid transparent');
    else {
        $(card).parents('.card-search-container').css('border', '1px solid darkgrey');
    }
    $(card).parents('.card-search').find(".back").toggle();
    $(card).parents('.card-search').find(".front").toggle();
}

submitStudent = function (div, number) {
    var modelIsValid = true;
    var input = $(div).parents('.card-search').find('.search-marks-input');
    for (var i = 0; i < input.length; i++) {
        if (input.eq(i).val() > 100 || input.eq(i).val() < 0) {
            input.eq(i).css({
                'border-bottom': '1px solid red !important',
                'box-shadow': '0 1px 0 0 red !important'
            });
            modelIsValid = false;
        }
    }
    var firstName = $(div).parents('.card-search').find('.search-firstName-input').eq(0);
    var lastName = $(div).parents('.card-search').find('.search-lastName-input').eq(0)
    var validateFirstName = firstName.val().split('');
    var validateLastName = lastName.val().split('');
    for (var i = 0; i < validateFirstName.length; i++) {
        if (validateFirstName[i] == '>' || validateFirstName[i] == '<') {
            modelIsValid = false;
            firstName.addClass('input-validation-error');
        }
    }
    for (var i = 0; i < validateLastName.length; i++) {
        if (validateLastName[i] == '>' || validateLastName[i] == '<') {
            modelIsValid = false;
            lastName.addClass('input-validation-error');
        }
    }

    if (modelIsValid) {
        swapCard(div, 'back');
        $.post('/student/update',
            $(div).parents(".back").serialize(),
            function (data) {
                if (data) {
                    for (var i = 0; i < $(div).parents('.card-search').find('.mark').length; i++) {
                        $(div).parents('.card-search').find('.mark').eq(i).html($(div).parents('.card-search').find('.search-marks-input').eq(i).val())
                    }
                }
            });
    }
}