using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TodoList.DTOs;
using TodoList.Services;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IValidator<TodoInsertDto> _todoInsertValidator;
        private readonly IValidator<TodoUpdateDto> _todoUpdateValidator;
        private readonly ICommonService<TodoDto, TodoInsertDto, TodoUpdateDto> _todoService;

        public TodoController(IValidator<TodoInsertDto> todoInsertValidator,
            IValidator<TodoUpdateDto> todoUpdateValidator,
           [FromKeyedServices("todoService")]ICommonService<TodoDto, TodoInsertDto, TodoUpdateDto> todoService)
        {
            _todoInsertValidator = todoInsertValidator;
            _todoUpdateValidator = todoUpdateValidator;
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoDto>> Get()
        {
            return await _todoService.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoDto>> GetById(int id)
        {
            var todoDto = await _todoService.GetById(id);
            
            return todoDto == null ? NotFound() : Ok(todoDto);
        }

        [HttpPost]
        public async Task<ActionResult<TodoDto>> Add(TodoInsertDto todoInsertDto)
        {
            var validationResult = await _todoInsertValidator.ValidateAsync(todoInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var todoDto = await _todoService.Add(todoInsertDto);
            
            return CreatedAtAction(nameof(GetById), new { id = todoDto.TodoId }, todoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoDto>> Update(int id, TodoUpdateDto todoUpdateDto)
        {
            var validationResult = await _todoUpdateValidator.ValidateAsync(todoUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var todoDto = await _todoService.Update(id, todoUpdateDto); 
            
            return Ok(todoDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoDto>> Delete(int id)
        {
            var todoDto = await _todoService.Delete(id);

            return Ok(todoDto);
        }
    }
}