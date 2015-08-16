define(['ko', 'app/ApiClient'], function (ko, apiClient) {
    function todoListViewModel() {
        var self = this;

        self.todoContent = ko.observable();
        self.todos = ko.observableArray([]);

        self.createTodo = function() {
            self.todos.push({ content: self.todoContent() });

            self.todoContent("");
        };

        apiClient.get('Todos', self.todos);
    };

    return todoListViewModel;
});