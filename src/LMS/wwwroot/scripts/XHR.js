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

    /// <summary>Initiates an ajax request</summary>
    /// <param name="obj" type="JS Object">A new JS object.</param>
    /// <param name="requestType></param>
    /// <param name="url"></param>
    /// <param name="async"></param>
    /// <param name="data"></param>
    /// <param name="responseType"></param>
    /// <param name="success"></param>
    /// <param name="error"></param>
    /// <returns>The rquested data</returns>
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
                    return error;
                }

                console.error("Server return an error");
            }
        };

        request.onerror = function () {
            // Connection error
            if (obj.error) {
                return error;
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