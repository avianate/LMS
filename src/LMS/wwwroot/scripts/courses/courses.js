(function (XHR, baseUtils) {
    "use strict";

    var templateUrl = "/app/courses/courses.html";
    var container = baseUtils.getContainer(".ajax-container");

    var coursesContainer = {};         // set after template is loaded
    var courseData;

    document.addEventListener("DOMContentLoaded", getTemplate());

    // load the html template
    function getTemplate() {
        baseUtils.getTemplate(templateUrl, loadContent);
    };

    function loadContent(data) {
        baseUtils.successHandler(container, data);
        baseUtils.useDarkNavbar(true);

        coursesContainer = baseUtils.getContainer("#courses");
        loadCourses();
    };

    // have template, will populate it
    // let's get the data
    function loadCourses() {
        XHR.get({
            url: "/api/course/latest",
            responseType: "JSON",
            success: templateIsReady
        });
    };

    function templateIsReady(data) {
        // save the data until the template is ready
        if (data !== null && data !== undefined) {
            courseData = data;
        }

        // check for template
        var template = baseUtils.getContainer("#courseTemplate");

        // continue polling until template is loaded and ready
        if (template === null) {
            setTimeout(templateIsReady, 500);
        } else {
            populatePreviews(courseData);
        }
    };

    function populatePreviews(data) {
        var template = baseUtils.getContainer("#courseTemplate");

        for (var course of data) {
            template.content.querySelector(".course-link").href = course.courseUrl;
            template.content.querySelector(".course-image").src = course.imageUrl;
            template.content.querySelector(".course-title").innerText = course.title;

            var clone = document.importNode(template.content, true);

            if (coursesContainer === null) {
                coursesContainer = baseUtils.getContainer("#courses");
            }

            coursesContainer.appendChild(clone);

            console.log(course);
        }
    };

})(XHR, baseUtils);