using System;
using System.Collections.Generic;

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

        public virtual void AddTodo(string content)
        {
            var todo = new Todo(Guid.NewGuid()) 
            {
                Content = content
            };

            _todos.Add(todo);
        }
    }
}
