var BLOG = (function (XHR) {
    var container = document.querySelector(".ajax-container");
    var postContainer = document.querySelector("#posts");

    var div = document.createElement("div");
    div.classList.add("row", "center", "middle", "full");

    var p = document.createElement("p");
    p.innerText = "This is a subheading created by a dynamically-loaded JS file";

    div.appendChild(p);
    container.appendChild(div);

    // have template, will populate it
    // let's get the data
    XHR.get({
        url: "/api/data/latest",
        responseType: "JSON",
        success: populatePreviews
    });

    function populatePreviews(data) {
        var template = document.querySelector("#blogPreviewTemplate");

        for (var post of data) {
            template.content.querySelector(".preview-image img").src = post.imageUrl;
            template.content.querySelector(".preview-content").innerText = post.body;

            var clone = document.importNode(template.content, true);
            postContainer.appendChild(clone);

            console.log(post);
        }
    };

    // publicly exposed methods
    return {
        // mapRoute: mapRoute,
        // route: route,
        // getRoutes: getRoutes
    };

}(XHR));