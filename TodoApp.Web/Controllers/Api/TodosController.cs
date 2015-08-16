using System;
using System.Web.Http;

namespace TodoApp.Web.Controllers.Api
{
    [Authorize]
    public class TodosController : ApiController
    {
        public IHttpActionResult Get()
        {
            var data = new[] {
                new {Id = Guid.NewGuid(), Content = "Buy cat food"},
                new {Id = Guid.NewGuid(), Content = "Vacuum the floor"},
                new {Id = Guid.NewGuid(), Content = "Mow the lawn"}
            };

            return Ok(data);
        }
    }
}
