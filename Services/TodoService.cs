using Microsoft.EntityFrameworkCore;
using TodoList.DTOs;
using TodoList.Models;
using TodoList.Repository;

namespace TodoList.Services;

public class TodoService : ICommonService<TodoDto, TodoInsertDto, TodoUpdateDto>
{
    private readonly IRepository<ToDo> _todoRepository;
    public TodoService(IRepository<ToDo> todoRepository)
    {
        _todoRepository = todoRepository;
    }
    
    public async Task<IEnumerable<TodoDto>> Get()
    {
        var todos = await _todoRepository.Get();

        return todos.Select(t => new TodoDto()
        {
            TodoId = t.TodoId,
            ToDoName = t.ToDoName,
            IsDone = t.IsDone,
            UserId = t.UserId
        });
    }

    public async Task<TodoDto> GetById(int id)
    {
        var todo = await _todoRepository.GetById(id);
        
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

        await _todoRepository.Add(todo);
        await _todoRepository.Save();

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
        var todo = await _todoRepository.GetById(id);
        if (todo != null)
        { 
            todo.ToDoName = todoUpdateDto.ToDoName;
            todo.IsDone = todoUpdateDto.IsDone;
            todo.UserId = todoUpdateDto.UserId;

             _todoRepository.Update(todo);
             await _todoRepository.Save();

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
        var todo = await _todoRepository.GetById(id);

        if (todo != null)
        {
            _todoRepository.Delete(todo);
            await _todoRepository.Save();

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