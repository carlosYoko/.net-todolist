using Microsoft.EntityFrameworkCore;
using TodoList.DTOs;
using TodoList.Models;

namespace TodoList.Services;

public class TodoService : ICommonService<TodoDto, TodoInsertDto, TodoUpdateDto>
{
    private readonly StoreContext _context;
    public TodoService(StoreContext context)
    {
        _context = context;
    }
    
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

    public async Task<TodoDto> GetById(int id)
    {
        var todo = await _context.ToDos!.FindAsync(id);
        
        if (todo != null)
        {
         var todoDto = new TodoDto()
            {
            TodoId = todo.TodoId,
            ToDoName = todo.ToDoName,
            IsDone = todo.IsDone,
            UserId = todo.UserId
             };
        return todoDto;
        }
        
        return null;
    }

    public async Task<TodoDto> Add(TodoInsertDto todoInsertDto)
    {
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

        return todoDto;
    }
    public async Task<TodoDto> Update(int id, TodoUpdateDto todoUpdateDto)
    {
        var todo = await _context.ToDos!.FindAsync(id);
        if (todo != null)
        { 
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
        return todoDto;
        }

        return null;
    }

    public async Task<TodoDto> Delete(int id)
    {
        var todo = await _context.ToDos!.FindAsync(id);

        if (todo != null)
        {
            _context.ToDos.Remove(todo);
            await _context.SaveChangesAsync();

        var todoDto = new TodoDto()
        {
            TodoId = todo.TodoId,
            ToDoName = todo.ToDoName,
            IsDone = todo.IsDone,
            UserId = todo.UserId    
        };
        
        return todoDto;
        }
        
        return null;
    }
}