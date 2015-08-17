/// <reference path="../lib/jasmine-2.3.4/jasmine.js" />
/// <reference path="../lib/require-2.1.20.js" />
/// <reference path="RequireExtensions.js" />
/// <reference path="MockApiClient.js" />

describe('TodoList View Model', function () {
    var todoListViewModel;
    var mockApiClient;

    beforeEach(function() {
        mockApiClient = new MockApiClient();
    });
    
    defineForTesting('app/ApiClient', function() { return mockApiClient; });

    requireForTesting('app/TodoListViewModel', function(sutType) {
         todoListViewModel = sutType;
    });

    it('should find the type', function() {
        expect(todoListViewModel).toBeDefined();
    });

    it('should add todo item to list', function () {
        var content = "Buy cat food";

        var sut = new todoListViewModel();

        sut.todoContent(content);
        sut.createTodo();

        expect(sut.todos()[0].content).toBe(content);
    });
    
    it('should persist created todo to model', function(done) {
        var content = "Mow the lawn";
        var posted = false;

        var predicate = function(controller, data) {
            var matches = controller == "Todos" && data.content == content;

            if (matches) {
                posted = true;
            }
            
            return matches;
        };

        mockApiClient.setupPost(predicate, null);

        var sut = new todoListViewModel();

        sut.todoContent(content);

        sut.createTodo();

        setTimeout(function () {
            expect(posted).toBe(true);
            done();
        });
    });

    it('should clear create todo text after adding todo', function() {
        var sut = new todoListViewModel();

        sut.todoContent("asdf");
        sut.createTodo();

        expect(sut.todoContent()).toBe("");
    });

    it('should populate todos from model', function(done) {
        var todos = [
            {}, {}, {}
        ];

        mockApiClient.setupGet(function (controller) { return controller == "Todos"; }, todos);

        var sut = new todoListViewModel();

        setTimeout(function () {
            var actualTodos = sut.todos();

            expect(actualTodos.length).toBe(todos.length);

            for (var i = 0; i < actualTodos.length; i++) {
                expect(actualTodos[i]).toBe(todos[i]);
            }
            
            done();
        });
    });
});