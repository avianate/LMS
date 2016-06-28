var XHR = (function () {
    "use strict";

    // private variables & functions

    // Example request Object:
    /*
        {
            requestType: "GET" or "POST" (default is GET),
            url: "http://theurl.com",
            async: false  default is true,
            data: postData
            responseType: "JSON" or Null for data,
            success: a callback function() {},
            error: a callback function () {}
        }
    */
    function get(obj) {
        var request = new XMLHttpRequest();
        var requestType = obj.requestType !== undefined ? obj.requestType : "GET";
        var async = obj.async || true;

        request.open(requestType, obj.url, async);

        request.onload = function () {
            if (request.status >= 200 && request.status < 400) {
                // success
                var response = request.responseText;

                if (obj.responseType != null && obj.responseType.toLowerCase() === "json") {
                    response = JSON.parse(response);
                }

                // return success handler or data
                if (obj.success != null && obj.success != "") {
                    return obj.success(response);
                } else {
                    return response;
                }

            } else {
                // reached the server but it returned an error
                if (obj.error) {
                    var response = JSON.parse(request.responseText);
                    var errors = response;

                    for (var error of errors) {
                        console.error(error.errorMessage);
                    }

                    return obj.error(errors);
                }
            }
        };

        request.onerror = function () {
            // Connection error
            if (obj.error) {
                var response = JSON.parse(request.responseText);
                var errors = response;

                for (var error of errors) {
                    console.error(error.errorMessage);
                }

                return obj.error;
            }

            console.error("Can't connect to server");
        };

        if (requestType.toLowerCase() === "post") {
            request.setRequestHeader("Content-Type", "application/json; charset=UTF-8");

            if (obj.data != null) {
                request.send(obj.data);
            } else {
                console.error("No data to post");
            }
        } else {

            request.send();
        }
    };

    // Example POST Object:
    /*
        {
            url: "http://theurl.com",
            async: false  (default is true)
            data: postData
            responseType: "JSON" or Null for data
            success: a callback function() {}
            error: a callback function () {}
        }
    */
    function post(obj) {
        obj.requestType = "POST";

        this.get(obj);
    };

    // publicly exposed functions & variables
    return {
        get: get,
        post: post
    };

})();