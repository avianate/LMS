(function (router, XHR) {
    "use strict";
        
    router.mapRoute({
        route: "/",
        templateUrl: "/app/default.html",
        default: true
    });
    
    router.mapRoute({
        route: "/blog",
        templateUrl: "/app/blog.html",
        controller: function (data) {
            handleResponse(data, "/scripts/blog/blog.js");
        }
    });
    
    router.mapRoute({
        route: "/courses",
        templateUrl: "/app/courses.html"
    });
    
    router.mapRoute({
        route: "/signup",
        templateUrl: "/app/account/register.html"
    });
    
    router.mapRoute({
        route: "/signin",
        templateUrl: "/app/account/signin.html"
    });
    
    router.mapRoute({
        route: "/signout",
        templateUrl: "/app/account/signout.html"
    });
    
    function handleResponse(data, scriptUrl) {

        var container = document.querySelector(".ajax-container");
        container.innerHTML = data;

        if (scriptUrl !== undefined || scriptUrl !== "") {
            var script = document.createElement("script");
            
            script.src = scriptUrl;
            document.body.appendChild(script);
        }
    }
    
})(router, XHR);