(function (XHR, baseUtils) {
    "use strict";

    var templateUrl = "/app/account/profile.html";
    var container = baseUtils.getContainer(".ajax-container");
    var dataUrl = "/account/profile";
    var template = {};
    var profileData = {};

    getData();
    //getTemplate();

    function getData() {
        XHR.get({
            requestType: "GET",
            url: dataUrl,
            success: successHandler,
            async: false
        });
    };

    function successHandler(data) {
        if (data !== null) {
            profileData = JSON.parse(data);
            getTemplate();
        }
    };

    function getTemplate() {
        baseUtils.getTemplate(templateUrl, loadContent);
    };

    function loadContent(data) {
        baseUtils.successHandler(container, data);
        baseUtils.useDarkNavbar(true);

        templateIsReady();
    }

    function templateIsReady(data) {
        // check for template
        template = baseUtils.getContainer("#profileTemplate");

        // continue polling until template is loaded and ready
        if (template === null) {
            setTimeout(templateIsReady, 500);
        } else {
            displayData();
        }
    };

    function displayData() {
        // get template
        template = template || baseUtils.getContainer("#profileTemplate");

        if (template !== null) {

            // populate template
            template.content.querySelector("#userName").innerText = profileData.userName;
            template.content.querySelector("#email").innerText = profileData.email;

            // clone template
            var clone = document.importNode(template.content, true);

            // append clone to DOM
            container.appendChild(clone);

            // log data
            console.log(profileData);
        }
    };

})(XHR, baseUtils);