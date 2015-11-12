function compareMarksModelDesc(left, right, categoryId ){

    var getMark = function (item) {
        return item.categoryId() == categoryId();
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

function compareMarksModelAsc(left, right, id) {

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

function compareByKeyDesc(left, right, key) {
    if (left[key]().toUpperCase() == right[key]().toUpperCase())
        return 0;
    else if (left[key]().toUpperCase() < right[key]().toUpperCase())
        return 1;
    else
        return -1;
}

function compareByKeyAsc(left, right, key) {
    if (left[key]().toUpperCase() == right[key]().toUpperCase())
        return 0;
    else if (left[key]().toUpperCase() < right[key]().toUpperCase())
        return -1;
    else
        return 1;
}

function compareStudentMarksByCategoryIdDesc(leftMark, rightMark) {
    if (leftMark.categoryId() == rightMark.categoryId())
        return 0;
    else if (leftMark.categoryId() < rightMark.categoryId())
        return -1;
    else
        return 1;
}