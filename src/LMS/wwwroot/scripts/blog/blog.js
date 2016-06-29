var BLOG = (function (XHR, baseUtils, router) {
    "use strict";
    
    var templateUrl = "/app/blog/blog.html";
    var container = baseUtils.getContainer(".ajax-container");

    var postContainer = {};         // set after template is loaded
    var postData;

    document.addEventListener("DOMContentLoaded", getTemplate());

    // load the html template
    function getTemplate() {
        baseUtils.getTemplate(templateUrl, loadContent);
    };

    function loadContent(data) {
        baseUtils.successHandler(container, data);
        baseUtils.useDarkNavbar(true);

        postContainer = baseUtils.getContainer("#posts");
        loadPosts();
    };

    // have template, will populate it
    // let's get the data
    function loadPosts() {
        XHR.get({
            url: "/api/data/latest",
            responseType: "JSON",
            success: templateIsReady
        });
    };

    function templateIsReady(data) {
        // save the data until the template is ready
        if (data !== null && data !== undefined) {
            postData = data;
        }

        // check for template
        var template = baseUtils.getContainer("#blogPreviewTemplate");

        // continue polling until template is loaded and ready
        if (template === null) {
            setTimeout(templateIsReady, 500);
        } else {
            populatePreviews(postData);
        }
    };

    function populatePreviews(data) {
        var template = baseUtils.getContainer("#blogPreviewTemplate");

        for (var post of data) {
            template.content.querySelector(".post-link").href = post.slug;
            template.content.querySelector(".preview-image img").src = post.imageUrl;
            template.content.querySelector(".post-link").innerText = post.title;

            var clone = document.importNode(template.content, true);

            if (postContainer === null) {
                postContainer = baseUtils.getContainer("#posts");
            }

            postContainer.appendChild(clone);

            console.log(post);
        }
    };

    function mapRoutes(posts) {

        for (var post of posts) {

            router.mapRoute({
                route: post.slug,
                controller: "/scripts/blog/post.js"
            });
        }
    }

}(XHR, baseUtils, router));