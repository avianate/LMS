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

    //router.mapRoute({
    //    route: "/signup",
    //    templateUrl: "/app/account/register.html",
    //    controller: function (data) {
    //        handleResponse(data);
    //        useDarkNavbar(true);
    //    }
    //});

    router.mapRoute({
        route: "/signin",
        templateUrl: "no-route"
    });

    router.mapRoute({
        route: "/signout",
        controller: "/scripts/account/signout.js"
    });

    router.mapRoute({
        route: "/profile",
        controller: "/scripts/account/profile.js"
        //templateUrl: "account/profile",
        //controller: function (data) {
        //    //handleResponse(data, "/scripts/accounts/profile");
        //    handleResponse(data);
        //    useDarkNavbar(false);
        //}
    });

})(router);