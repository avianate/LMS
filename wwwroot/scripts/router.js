var router = (function (XHR) {
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
    initLinks();
    
    // hijack click events on all anchor tags to prevent browser from loading their URLs
    // we want our client side router to handle the links
    function initLinks () {
        var links = document.querySelectorAll("a"),
            length = links.length,
            i = 0,
            link;
            
        for (; i < length; i++) {
            link = links[i];
            
            link.addEventListener("click", function (e) {
                e.preventDefault();
                e.stopPropagation();
                var container = getContainer();
                container.classList.add("fade-out");
                pushHistory(e.currentTarget);
            });
        }
    }
    
    function pushHistory(target) {
        var targetUrl = target.getAttribute("href");
        var prettyUrl = targetUrl === "/" ? "/" : targetUrl.replace("/", "");
        
        history.pushState(prettyUrl, prettyUrl, prettyUrl);
        document.title = targetUrl.replace("/", "");
        route();
    };
    
    function replaceHistory(title) {
        history.replaceState(title, title, title);
        document.title = title;
    }
    
    /*
        obj.route                // the brower route we want to map to our client side router
        obj.templateUrl          // the url to get the html file from the server or the url to hit the server's controller and action method
        obj.controller           // function used for setting properties or passing data to the template
        obj.container            // the container element's tag, ID, or class
        obj.default              // a boolean flag to set this as the default route
    */
    // method to add a new client side route
    function mapRoute (obj) {
        var thisObject = obj.route.toLowerCase();
        
        routes[thisObject] = { 
            templateUrl: obj.templateUrl || obj.route.toLowerCase(), 
            controller: obj.controller, 
            container: obj.container || defaultContainer
        };
        
        if (obj.default) {
            defaultRoute = routes[thisObject];
        }
    };
    
    /*
        checks if the browser's location has a mapped route
        and retrieves the data from the server and passes it
        to the controller function for the route
    */
    function route () {
        var url,
            container,
            controller;
            
        url = location.pathname.toLowerCase() || "/";
        currentRoute = routes[url] || "";
            
        // if we don't have a valid route object return home
        if (currentRoute === "" || !currentRoute) {
            currentRoute = defaultRoute;
        }

        controller = currentRoute.controller || setContent;
        
        // if we have a route object and it's container element
        if (currentRoute.templateUrl !== null && currentRoute.templateUrl !== "") {   

            // get the data from the templateUrl
            XHR.get({
                requestType: "GET",
                url: currentRoute.templateUrl,
                success: controller
            });
            
            replaceHistory(url.replace("/", ""));
        }
    };
    
    function setContent(data) {
        var container = getContainer(currentRoute.container);
        
        if (container) {
            container.innerHTML = data;
        } else {
            console.log("No container to place content into")
        }
    }

    function getContainer(selector) {
        var container = selector !== "" 
                        && selector !== undefined 
                        && selector !== null 
                            ? document.querySelector(selector) 
                            : document.querySelector(defaultContainer);

        return container;
    }
    
    function getRoutes () {
        return routes;
    };
    
    return {
        mapRoute: mapRoute,
        route: route,
        getRoutes: getRoutes
    };
    
})(XHR);