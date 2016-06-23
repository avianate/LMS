(function (router, XHR) {
    "use strict";

    var container = getAjaxContainer();

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

    function handleResponse(data, scriptUrl) {
        if (container.classList.contains("fade-out")) {
            container.addEventListener("animationend", function loadData(e) {
                if (e.animationName === "fadeOut") {
                    setData(data);
                    setScripts(scriptUrl);
                    fadeIn();
                    container.removeEventListener("animationend", loadData);
                }
            });
        } else {
            setData(data);
            setScripts(scriptUrl);
        }
    };

    function setData(data) {
        container.innerHTML = data;
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
            container.classList.remove("fade-out");
            container.classList.add("fade-in");
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
    };

})(router, XHR);