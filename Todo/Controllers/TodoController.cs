using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController
        ( ILogger<TodoController> logger
        , AppDbContext context) : ControllerBase
    {
        private readonly ILogger<TodoController> _logger = logger;
        private readonly AppDbContext _context = context;

        [HttpGet(Name = "GetTodo")]
        public ActionResult<IEnumerable<TodoModel>> Get()
        {
            _logger.Log(LogLevel.Information, "Getting todo list");
            return Ok(_context.Todos.ToList());
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public ActionResult<TodoModel> Get
            ( 
                int id 
            )
        {
            _logger.Log(LogLevel.Information, "Getting todo by id");
            var todo = _context.Todos.FirstOrDefault(todo => todo.Id == id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }


        [HttpPost(Name = "CreateTodo")]
        public ActionResult<TodoModel> Create
            (
              [FromBody] TodoModel todo
            )
        {
            _logger.Log(LogLevel.Information, "Creating todo");
            _context.Todos.Add(todo);
            _context.SaveChanges();
            return CreatedAtRoute("GetById", new { id = todo.Id }, todo);
        }

        [HttpPut("{id:int}", Name = "UpdateTodo")]
        public ActionResult<TodoModel> Update
            (
              int id
            , [FromBody] TodoModel todo
            )
        {
            _logger.Log(LogLevel.Information, "Updating todo");
            var existingTodo = _context.Todos.FirstOrDefault(todo => todo.Id == id);
            if (existingTodo == null) return NotFound();
            
            existingTodo.Title = todo.Title;
            existingTodo.Done = todo.Done;

            _context.Todos.Update(existingTodo);
            _context.SaveChanges();
            return Ok(existingTodo);
        }

        [HttpDelete("{id:int}", Name = "DeleteTodo")]
        public ActionResult Delete
            (
              int id
            )
        {
            _logger.Log(LogLevel.Information, "Deleting todo");
            var todo = _context.Todos.FirstOrDefault(todo => todo.Id == id);
            if (todo == null) return NotFound();

            _context.Todos.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
