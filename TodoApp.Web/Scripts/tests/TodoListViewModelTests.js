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

        var predicate = function(url, data) {
            var matches = url == "Todos" && data.content == content;

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
            { id: 1, content: 'hello', isCompleted: false },
            { id: 2, content: 'world', isCompleted: true }
        ];

        mockApiClient.setupGet(function (url) { return url == "Todos"; }, todos);

        var sut = new todoListViewModel();

        setTimeout(function () {
            var actualTodos = sut.todos();

            expect(actualTodos.length).toBe(todos.length);

            expect(actualTodos[0].id).toBe(todos[0].id);
            expect(actualTodos[1].id).toBe(todos[1].id);
            
            expect(actualTodos[0].content).toBe(todos[0].content);
            expect(actualTodos[1].content).toBe(todos[1].content);
            
            expect(actualTodos[0].isCompleted()).toBe(todos[0].isCompleted);
            expect(actualTodos[1].isCompleted()).toBe(todos[1].isCompleted);

            done();
        });
    });

    cases('should mark todo as completed', [true, false], function (newState, done) {
        var isPut = false;

        var predicate = function (url, data) {
            if (url == "Todos/abcd" && data.isCompleted === newState) {
                isPut = true;
                return true;
            }

            return false;
        };

        mockApiClient.setupGet(function (controller) { return controller == "Todos"; }, [{ id: 'abcd', isCompleted: !newState }]);
        mockApiClient.setupPut(predicate, null);

        var sut = new todoListViewModel();

        setTimeout(function () {
            var todo = sut.todos()[0];
            todo.isCompleted(newState);
            
            setTimeout(function () {
                expect(isPut).toBe(true);
                done();
            });
        });
    });
});