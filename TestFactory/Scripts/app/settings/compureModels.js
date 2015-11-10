function compareMarksModelDesc(left, right) {

    var getMark = function (item) {
        return item.categoryId() == id();
    }
    var leftMark = ko.utils.arrayFilter(left.marks(), getMark)[0];
    var rightMark = ko.utils.arrayFilter(right.marks(), getMark)[0];

    if (leftMark.value() == rightMark.value())
        return 0;
    else if (leftMark.value() < rightMark.value())
        return 1;
    else
        return -1;
}

function compareMarksModelAsc(left, right) {

    var getMark = function (item) {
        return item.categoryId() == id();
    }

    var leftMark = ko.utils.arrayFilter(left.marks(), getMark)[0];
    var rightMark = ko.utils.arrayFilter(right.marks(), getMark)[0];

    if (leftMark.value() == rightMark.value())
        return 0;
    else if (leftMark.value() < rightMark.value())
        return -1;
    else
        return 1;
}