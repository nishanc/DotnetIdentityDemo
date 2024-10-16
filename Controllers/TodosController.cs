using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetIdentityDemo.Data;
using DotnetIdentityDemo.Models;
using DotnetIdentityDemo.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace DotnetIdentityDemo.Controllers
{
    [Authorize] // Ensure all endpoints require authentication
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TodosController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/todos
        [HttpGet]
        [Authorize(Roles = "RegisteredUser,Administrator")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET: api/todos/5
        [HttpGet("{id}")]
        [Authorize(Roles = "RegisteredUser,Administrator")]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // POST: api/todos
        [HttpPost]
        [Authorize(Roles = "RegisteredUser,Administrator")]
        public async Task<ActionResult<Todo>> CreateTodo(TodoCreateDto todoDto)
        {
            // Map the DTO to the Todo model
            var todo = new Todo
            {
                Name = todoDto.Name,
                IsComplete = todoDto.IsComplete
            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        // PUT: api/todos/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateTodoName(int id, string name)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/todos/5/complete
        [HttpPut("{id}/complete")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> MarkTodoAsComplete(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/todos/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteTodoById(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
