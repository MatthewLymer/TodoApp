define(['ko', 'app/ApiClient', 'jquery'], function (ko, apiClient, $) {
    function todoViewModel(data) {
        var self = this;
        self.id = data.id;
        self.content = data.content;
        self.isCompleted = ko.observable(data.isCompleted);

        self.isCompleted.subscribe(function (newValue) {
            apiClient.put('Todos/' + self.id, { isCompleted: newValue }, function() {});
        });
    }

    function todoListViewModel() {
        var self = this;

        self.todoContent = ko.observable();
        self.todos = ko.observableArray([]);

        self.createTodo = function() {
            var todo = new todoViewModel({ content: self.todoContent() });
            
            self.todos.push(todo);

            apiClient.post('Todos', todo, function(data) { todo.id = data.id; });

            self.todoContent("");
        };

        apiClient.get('Todos', function (data) {
            var items = $.map(data, function(item) { return new todoViewModel(item); });
            self.todos(items);
        });
    };

    return todoListViewModel;
});