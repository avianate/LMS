(function (XHR, baseUtils) {
    "use strict";

    var templateUrl = "/app/default.html";
    var container = baseUtils.getContainer(".ajax-container");

    getTemplate();

    function getTemplate() {
        baseUtils.getTemplate(templateUrl, loadContent);
    };

    function loadContent(data) {
        baseUtils.successHandler(container, data);
        baseUtils.changeNavbar(false);
    };

})(XHR, baseUtils);