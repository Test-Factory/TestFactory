var settings = {
    basePath: getBasePath()
};

function getBasePath() {
    var pathArray = location.href.split('/');
    var protocol = pathArray[0];
    var host = pathArray[2];
    var url = protocol + '//' + host;
    return url;
}