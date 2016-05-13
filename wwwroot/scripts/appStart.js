(function (router, XHR) {
    "use strict";
        
    router.mapRoute({
        route: "/",
        templateUrl: "",
        controller: handleDefaultResponse
    });
    
    router.mapRoute({
        route: "/blog",
        templateUrl: "/app/blog.html"
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
    
    function handleDefaultResponse() {
        var container = document.querySelector("ajax-container");
        container.innerHTML = "";
    }
    
})(router, XHR);