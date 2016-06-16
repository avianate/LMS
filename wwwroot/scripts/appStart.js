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
        templateUrl: "/app/courses.html",
        controller: function (data) {
            handleResponse(data);
        }
    });
    
    router.mapRoute({
        route: "/signup",
        templateUrl: "/app/account/register.html",
        controller: function (data) {
            handleResponse(data);
        }
    });
    
    router.mapRoute({
        route: "/signin",
        templateUrl: "/app/account/signin.html",
        controller: function (data) {
            handleResponse(data);
        }
    });
    
    router.mapRoute({
        route: "/signout",
        templateUrl: "/app/account/signout.html",
        controller: function (data) {
            handleResponse(data);
        }
    });
    
    function handleResponse(data, scriptUrl) {

        var container = document.querySelector(".ajax-container");
        container.innerHTML = data;

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
    }
    
})(router, XHR);