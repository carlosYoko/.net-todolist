using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.DTOs;
using TodoList.Models;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IValidator<TodoInsertDto> _todoInsertValidator;
        private readonly IValidator<TodoUpdateDto> _todoUpdateValidator;

        public TodoController(StoreContext context,
            IValidator<TodoInsertDto> todoInsertValidator,
            IValidator<TodoUpdateDto> todoUpdateValidator)
        {
            _context = context;
            _todoInsertValidator = todoInsertValidator;
            _todoUpdateValidator = todoUpdateValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoDto>> Get()
        {
            return await _context.ToDos!.Select(t => new TodoDto()
            {
                TodoId = t.TodoId,
                ToDoName = t.ToDoName,
                IsDone = t.IsDone,
                UserId = t.UserId
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetById(int id)
        {
            var todo = await _context.ToDos!.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            var todoDto = new TodoDto()
            {
                TodoId = todo.TodoId,
                ToDoName = todo.ToDoName,
                IsDone = todo.IsDone,
                UserId = todo.UserId
            };

            return Ok(todoDto);
        }

        [HttpPost]
        public async Task<ActionResult<TodoDto>> Add(TodoInsertDto todoInsertDto)
        {
            var validationResult = await _todoInsertValidator.ValidateAsync(todoInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var todo = new ToDo()
            {
                ToDoName = todoInsertDto.ToDoName,
                IsDone = todoInsertDto.IsDone,
                UserId = todoInsertDto.UserId
            };

            await _context.ToDos!.AddAsync(todo);
            await _context.SaveChangesAsync();

            var todoDto = new TodoDto()
            {
                TodoId = todo.TodoId,
                ToDoName = todo.ToDoName,
                IsDone = todo.IsDone,
                UserId = todo.UserId
            };

            return CreatedAtAction(nameof(GetById), new { id = todo.TodoId }, todoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoDto>> Update(int id, TodoUpdateDto todoUpdateDto)
        {
            var validationResult = await _todoUpdateValidator.ValidateAsync(todoUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            
            var todo = await _context.ToDos!.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.ToDoName = todoUpdateDto.ToDoName;
            todo.IsDone = todoUpdateDto.IsDone;
            todo.UserId = todoUpdateDto.UserId;
            await _context.SaveChangesAsync();

            var todoDto = new TodoDto()
            {
                TodoId = todo.TodoId,
                ToDoName = todo.ToDoName,
                IsDone = todo.IsDone,
                UserId = todo.UserId    
            };

            return Ok(todoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var todo = await _context.ToDos!.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            _context.ToDos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}