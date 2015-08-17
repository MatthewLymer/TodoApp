define(['jquery', 'config'], function ($, config) {
    function doRequest(method, controller, data, success) {
        $.ajax({
            type: method,
            url: config.apiUrl + controller,
            data: data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: success
        });
    }

    return {        
        get: function (url, success) {
            doRequest("GET", url, undefined, success);
        },

        post: function (url, data, success) {
            doRequest("POST", url, JSON.stringify(data), success);
        },
        
        'delete': function (url, success) {
            doRequest("DELETE", url, undefined, success);
        }
    };
});