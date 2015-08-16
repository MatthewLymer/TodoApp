using System;

namespace TodoApp.Core.Entities
{
    public class Todo
    {
        private readonly Guid _id;

        public Todo(Guid id)
        {
            _id = id;
        }

        protected Todo()
        {
            
        }

        public virtual Guid Id { get { return _id; } }
        public virtual string Content { get; set; }
    }
}