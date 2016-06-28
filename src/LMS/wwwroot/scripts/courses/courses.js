(function (XHR, baseUtils) {
    "use strict";

    var templateUrl = "/app/courses/courses.html";
    var container = baseUtils.getContainer(".ajax-container");

    getTemplate();

    function getTemplate() {
        baseUtils.getTemplate(templateUrl, loadContent);
    };

    function loadContent(data) {
        baseUtils.successHandler(container, data);
        baseUtils.useDarkNavbar(true);
    };

})(XHR, baseUtils);