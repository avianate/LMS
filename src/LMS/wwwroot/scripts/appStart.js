(function (router, XHR) {
    "use strict";

    var container = getAjaxContainer();
    container.addEventListener("transitionend", fadeIn);

    // ROUTES
    router.mapRoute({
        route: "/",
        templateUrl: "/app/default.html",
        controller: function (data) {
            handleResponse(data);
            changeNavbar(false);
        },
        default: true
    });

    router.mapRoute({
        route: "/blog",
        templateUrl: "/app/blog/blog.html",
        controller: function (data) {
            handleResponse(data, "/scripts/blog/blog.js");
            changeNavbar(true);
        }
    });

    router.mapRoute({
        route: "/courses",
        templateUrl: "/app/courses.html",
        controller: function (data) {
            handleResponse(data);
            changeNavbar(true);
        }
    });

    router.mapRoute({
        route: "/signup",
        templateUrl: "/app/account/register.html",
        controller: function (data) {
            handleResponse(data);
            changeNavbar(true);
        }
    });

    router.mapRoute({
        route: "/signin",
        templateUrl: "/app/account/signin.html",
        controller: function (data) {
            handleResponse(data);
            changeNavbar(true);
        }
    });

    router.mapRoute({
        route: "/signout",
        templateUrl: "/app/account/signout.html",
        controller: function (data) {
            handleResponse(data);
            changeNavbar(true);
        }
    });

    // FUNCTIONS

    // Handles display of the ajax data 
    function handleResponse(data, scriptUrl) {
        if (container.classList.contains("fade-out")) {
            container.addEventListener("load", function loadData() {
                container.innerHTML = data;
                setScripts(scriptUrl);
                container.removeEventListener("load", loadData);
            });
        } else {
            container.innerHTML = data;
            setScripts(scriptUrl);
        }
    };

    // Dynamic script loading
    function setScripts(scriptUrl) {
        var script = document.querySelector("#dynamic");

        if (script !== undefined && script !== null) {
            document.body.removeChild(script);
        }

        if (scriptUrl !== "" && scriptUrl !== undefined && scriptUrl !== null) {
            var newScript = document.createElement("script");

            newScript.id = "dynamic";
            newScript.src = scriptUrl;

            document.body.appendChild(newScript);
        }
    };

    // Fade in ajax data
    function fadeIn() {
        if (container.classList.contains("fade-out")) {
            var event = new CustomEvent("load");
            container.dispatchEvent(event);

            container.classList.remove("fade-out");
        }
    };

    // change navbar link colors to light or dark
    function changeNavbar(shouldBeDark) {
        var navbar = document.querySelector("div.nav-bar.center-container");
        var isDark = navbar.classList.contains("dark") ? true : false;

        if (!isDark && shouldBeDark) {
            navbar.classList.add("dark");
        } else if (isDark && !shouldBeDark) {
            navbar.classList.remove("dark");
        }
    };

    // Get the ajax container
    function getAjaxContainer() {
        return document.querySelector(".ajax-container");
    }

})(router, XHR);