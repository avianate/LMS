var baseUtils = (function (XHR) {
    "use strict";

    var isAnimating = false;

    // *****************************************************
    // PRIVATE FUNCTIONS
    // *****************************************************

    // initialize the container animation listeners
    function init(container) {
        container.addEventListener("animationstart", toggleAnimating);
    };

    // set the ajax data into the container
    function setData(container, data) {
        container.innerHTML = data;
    };

    // set the isAnimating flag
    function toggleAnimating(e) {
        if (e.type === "animationstart") {
            isAnimating = true;
        } else {
            isAnimating = false;
        }
    };

    // *****************************************************
    // PUBLIC FUNCTIONS
    // *****************************************************
    function handleResponse(container, data) {
        // just got the data, init the listeners
        init(container);

        if (!hasClass(container, "fade-out")) {
            // fade out the container
            fadeOut(container);
        }

        if (hasClass(container, "fade-out")) {

            if (isAnimating) {

                // wait for animation to finish
                container.addEventListener("animationend", function loadData(e) {
                    if (e.animationName === "fadeOut") {

                        // set the content
                        setData(container, data);

                        // fade the container back in
                        fadeIn(container);

                        // clean up
                        container.removeEventListener("animationend", loadData);
                        isAnimating = false;
                    }
                });
            } else {
                setData(container, data);
                fadeIn(container);
            }
        } else {
            setData(container, data);
        }

        return;
    };

    // check if an element has the specified css class
    function hasClass(container, className) {
        return container.classList.contains(className);
    };

    // Fade in ajax data
    function fadeIn(container) {
        if (hasClass(container, "fade-out")) {
            container.classList.remove("fade-out");
            container.classList.add("fade-in");
        } else {
            container.classList.add("fade-in");
        }
    };

    // Fade out container
    function fadeOut(container) {
        if (hasClass(container, "fade-in")) {
            container.classList.remove("fade-in");
            container.classList.add("fade-out");
        } else {
            container.classList.add("fade-out");
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

    function getContainer(selector) {
        var container;

        if (selector !== "" && selector !== undefined && selector !== null) {
            container = document.querySelector(selector);
        }

        return container;
    };

    function getTemplate(templateUrl, callback) {
        XHR.get({
            requestType: "GET",
            url: templateUrl,
            success: callback || handleResponse
        });
    }

    // *****************************************************
    // PUBLICLY EXPOSED FUNCTIONS / VARIABLES
    // *****************************************************
    return {
        successHandler: handleResponse,
        hasClass: hasClass,
        fadeIn: fadeIn,
        fadeOut: fadeOut,
        useDarkNavbar: changeNavbar,
        getContainer: getContainer,
        getTemplate: getTemplate
    };

})(XHR);