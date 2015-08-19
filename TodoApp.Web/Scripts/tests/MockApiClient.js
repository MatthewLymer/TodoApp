(function() {
    function mockApiClient() {
        var responses = [];
        
        function findResponse(method, controller, data) {
            for (var i = 0; i < responses.length; i++) {
                var response = responses[i];
                
                if (response.method == method && response.predicate(controller, data)) {
                    return response;
                }
            }

            return undefined;
        }

        this.get = function(url, success) {
            var response = findResponse('GET', url);
            if (response) {
                success(response.data);
            }
        };

        this.post = function(url, data, success) {
            var response = findResponse('POST', url, data);
            if (response) {
                success(response.data);
            }
        };
        
        this.put = function (url, data, success) {
            var response = findResponse('PUT', url, data);
            if (response) {
                success(response.data);
            }
        };

        this.delete = function(url, success) {
            var response = findResponse('DELETE', url);
            if (response) {
                success(response.data);
            }
        };
        
        this.setupGet = function (predicate, data) {
            responses.push({ method: 'GET', predicate: predicate, data: data });
        };

        this.setupPost = function(predicate, data) {
            responses.push({ method: 'POST', predicate: predicate, data: data });
        };
        
        this.setupPut = function (predicate, data) {
            responses.push({ method: 'PUT', predicate: predicate, data: data });
        };

        this.setupDelete = function(pridicate, data) {
            responses.push({ method: 'DELETE', predicate: predicate, data: data });
        };
    }

    window.MockApiClient = mockApiClient;
})();