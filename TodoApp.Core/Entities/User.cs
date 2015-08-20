using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TodoApp.Core.Entities
{
    public class User
    {
        private readonly IList<Todo> _todos;
        private readonly Guid _id;

        public User(Guid id)
        {
            _id = id;
            _todos = new List<Todo>();
        }

        protected User()
        {
            
        }

        public virtual Guid Id
        {
            get { return _id; }
        }

        public virtual string Email
        {
            get; protected set;
        }

        public virtual IReadOnlyCollection<Todo> Todos
        {
            get { return new ReadOnlyCollection<Todo>(_todos); }
        }

        public virtual Todo CreateTodo(string content)
        {
            var todo = new Todo(Guid.NewGuid()) 
            {
                Content = content,
                IsCompleted = false,
                DateCreated = DateTime.UtcNow
            };

            _todos.Add(todo);

            return todo;
        }
    }
}
