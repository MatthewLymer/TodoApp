using System;
using System.Linq;
using System.Web.Http;
using TodoApp.Core.Entities;
using TodoApp.Core.Repositories;

namespace TodoApp.Web.Controllers.Api
{
    [Authorize]
    public class TodosController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public TodosController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IHttpActionResult Get()
        {
            var user = GetCurrentUser();

            return Ok(user.Todos);
        }

        public IHttpActionResult Post(PostTodoRequest request)
        {
            var user = GetCurrentUser();

            var todo = user.CreateTodo(request.Content);

            _userRepository.Save(user);

            return Created("", new PostTodoResponse{Id = todo.Id});
        }

        public IHttpActionResult Put(Guid id, PutTodoRequest request)
        {
            var user = GetCurrentUser();

            var todo = user.Todos.Single(x => x.Id == id);

            todo.IsCompleted = request.IsCompleted;

            _userRepository.Save(user);

            return Ok();
        }

        private User GetCurrentUser()
        {
            return _userRepository.GetByEmail(User.Identity.Name);
        }
    }

    public class PutTodoRequest
    {
        public bool IsCompleted { get; set; }
    }

    public class PostTodoRequest
    {
        public string Content { get; set; }
    }

    public class PostTodoResponse
    {
        public Guid Id { get; set; }
    }
}
