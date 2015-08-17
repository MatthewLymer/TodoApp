define(['ko', 'app/ApiClient'], function (ko, apiClient) {
    function todoListViewModel() {
        var self = this;

        self.todoContent = ko.observable();
        self.todos = ko.observableArray([]);

        self.createTodo = function() {
            var todo = { content: self.todoContent() };
            
            self.todos.push(todo);

            apiClient.post('Todos', todo, function() {});

            self.todoContent("");
        };

        apiClient.get('Todos', self.todos);
    };

    return todoListViewModel;
});