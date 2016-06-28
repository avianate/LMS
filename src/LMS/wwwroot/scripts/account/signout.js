(function (XHR, baseUtils) {
    "use strict";

    var templateUrl = "";
    var container = {};

    init();

    function init() {
        templateUrl = "app/default.html";
        container = baseUtils.getContainer(".ajax-container");

        signOut();
    };
    
    function signOut() {
        XHR.get({
            requestType: "GET",
            url: "account/signout",
            success: goHome
        });
    };

    function goHome() {
        location = "/";
    };

})(XHR, baseUtils);