using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoApp.API.Attributes;
using TodoApp.Core.Managers.Interfaces;
using TodoApp.ModelView.Request;

namespace TodoApp.API.Controllers
{
    [ApiController]
    public class TodoController : ApiBaseController
    {
        private ITodoManager _todoManager;
        private readonly ILogger<UserController> _logger;

        public TodoController(ITodoManager todoManager, ILogger<UserController> logger)
        {
            _todoManager = todoManager;
            _logger = logger;
        }

        [Route("api/todo/create")]
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CreateTodo(TodoRequest todoRequest)
        {
            var result = _todoManager.CreateTodo(LoggedInUser, todoRequest);
            return Ok(result);
        }

        [Route("api/todo/getall")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetTodos(int page = 1, int pageSize = 10, string sortColumn = "", string sortDirection = "ascending", string searchText = "")
        {
            var result = _todoManager.GetTodos(page, pageSize, sortColumn, sortDirection, searchText);
            return Ok(result);
        }

        [Route("api/todo/get/{id}")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetTodo(int id)
        {
            var result = _todoManager.GetTodo(id);
            return Ok(result);
        }

        [Route("api/todo/edit")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult PutTodo(TodoRequest todoRequest)
        {
            var result = _todoManager.PutTodo(LoggedInUser, todoRequest);
            return Ok(result);
        }

        [Route("api/todo/assign")]
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [TodoAppAuthorize()]
        public IActionResult AssignTodo(TodoAssign todoAssign)
        {
            var result = _todoManager.AssignTodo(LoggedInUser, todoAssign);
            return Ok(result);
        }

        [Route("api/todo/delete/{id}")]
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [TodoAppAuthorize()]
        public IActionResult ArchiveTodo(int id)
        {
            _todoManager.ArchiveTodo(LoggedInUser, id);
            return Ok();
        }
    }
}
