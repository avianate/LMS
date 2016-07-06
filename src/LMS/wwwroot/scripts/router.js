var router = (function (XHR, baseUtils) {
    "use strict";

    // the client side routes
    var routes = {};
    var defaultRoute = {};
    var currentRoute = {};
    var defaultContainer = ".ajax-container";
    var transitionHasEnded = true;

    // listen for back / forward browser buttons
    window.addEventListener("popstate", route);
    window.addEventListener("DOMContentLoaded", route);

    function startRouting(e) {
        e.preventDefault();
        e.stopPropagation();

        // if there is a data-no-route attribute on the anchor tag,
        // it's controller will handle the events
        if (e.target.hasAttribute("data-no-route")) {
            return;
        }

        pushHistory(e.currentTarget);
    };

    // updates the browser history
    function pushHistory(target) {
        var targetUrl = target.getAttribute("href");
        var prettyUrl = targetUrl === "/" ? "/" : targetUrl.replace("/", "");

        // if we're already on the page, no need to continue
        if (targetUrl === location.pathname) {
            return;
        }

        history.pushState(prettyUrl, prettyUrl, prettyUrl);
        document.title = targetUrl.replace("/", "");
        route();
    };

    // updates the browser history
    function replaceHistory(title) {
        history.replaceState(title, title, title);
        document.title = title;
    };

    /*
        obj.route                // the brower route we want to map to our client side router
        obj.templateUrl          // the url to get the html file from the server or the url to hit the server's controller and action method
        obj.controller           // function used for setting properties or passing data to the template
        obj.container            // the container element's tag, ID, or class
        obj.default              // a boolean flag to set this as the default route
    */
    // method to add a new client side route
    function mapRoute(obj) {
        var thisObject = obj.route.toLowerCase(),
            href = "a[href='{url}']",
            links = {};

        routes[thisObject] = {
            //templateUrl: obj.templateUrl || obj.route.toLowerCase(),
            controller: obj.controller,
            //container: obj.container || defaultContainer
        };

        if (obj.default) {
            defaultRoute = routes[thisObject];
        }
        
        href = href.replace("{url}", thisObject);
        links = document.querySelectorAll(href);

        initLinks(links);
    };

    function initLinks(links) {
        for (var link of links) {
            link.addEventListener("click", startRouting);
        }
    };

    /*
        checks if the browser's location has a mapped route
        and retrieves the data from the server and passes it
        to the controller function for the route
    */
    function route(e) {
        var url,
            container,
            controller;

        // add fade in / out effect if it was a browser back / forward navigation
        if (e !== undefined && e.type === "popstate") {
            var container = getContainer(defaultContainer);
            baseUtils.fadeOut(container);
        }

        url = location.pathname.toLowerCase() || "/";
        currentRoute = routes[url] || "";

        // if we don't have a valid route object return home
        if (currentRoute === "" || !currentRoute) {
            currentRoute = defaultRoute;
        }

        controller = currentRoute.controller;        

        if (typeof(controller) === "string") {
            addControllerToDOM(controller);
        } else if (typeof(controller) === "function") {
            controller();
        }
        //// get the data from the templateUrl
        //XHR.get({
        //    requestType: "GET",
        //    url: currentRoute.controller,
        //    success: addControllerToDOM
        //});

        replaceHistory(url.replace("/", ""));
    };

    // Dynamic script loading
    function addControllerToDOM(scriptUrl) {
        var script = document.querySelector("#dynamic");

        if (script !== undefined && script !== null) {
            if (document.body.contains(script)) {
                document.body.removeChild(script);
            }
        }

        if (scriptUrl !== "" && scriptUrl !== undefined && scriptUrl !== null) {
            var newScript = document.createElement("script");

            newScript.id = "dynamic";
            newScript.src = scriptUrl;

            document.body.appendChild(newScript);
        }
    };

    // returns the object for the passed in selector
    // will grab the defaultContainer if no selector is passed in
    function getContainer(selector) {
        return baseUtils.getContainer(selector);
    };

    // gets the routes
    function getRoutes() {
        return routes;
    };

    // publicly exposed methods
    return {
        mapRoute: mapRoute,
        route: route,
        getRoutes: getRoutes
    };

})(XHR, baseUtils);