using Microsoft.AspNetCore.Mvc;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController(ILogger<TodoController> logger) : ControllerBase
    {
        private readonly ILogger<TodoController> _logger = logger;

        [HttpGet(Name = "GetTodo")]
        public IEnumerable<TodoModel> Get()
        {
            _logger.Log(LogLevel.Information, "Getting todo list");
            return [];
        }
    }
}
