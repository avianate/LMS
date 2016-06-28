(function (router) {
    "use strict";
    
    // ROUTES
    router.mapRoute({
        route: "/",
        controller: "/scripts/default.js",
        default: true
    });

    router.mapRoute({
        route: "/blog",
        controller: "/scripts/blog/blog.js"
    });

    router.mapRoute({
        route: "/courses",
        controller: "/scripts/courses/courses.js"
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
        templateUrl: "no-route"
    });

    router.mapRoute({
        route: "/signout",
        controller: "/scripts/account/signout.js"
        //templateUrl: "account/signout",
        //controller: function (data) {
        //    // redirect to home
        //    location = "/";
        //}
    });

    router.mapRoute({
        route: "/profile",
        templateUrl: "account/profile",
        controller: function (data) {
            //handleResponse(data, "/scripts/accounts/profile");
            handleResponse(data);
            changeNavbar(false);
        }
    });

})(router);