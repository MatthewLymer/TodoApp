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

        this.get = function(controller, success) {
            var response = findResponse('GET', controller);
            if (response) {
                success(response.data);
            }
        };

        this.post = function(controller, data, success) {
            var response = findResponse('POST', controller, data);
            if (response) {
                success(response.data);
            }
        };

        this.delete = function(controller, success) {
            var response = findResponse('DELETE', controller);
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

        this.setupDelete = function(pridicate, data) {
            responses.push({ method: 'DELETE', predicate: predicate, data: data });
        };
    }

    window.MockApiClient = mockApiClient;
})();