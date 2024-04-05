using AutoMapper;
using TodoList.DTOs;
using TodoList.Models;
using TodoList.Repository;

namespace TodoList.Services;

public class TodoService : ICommonService<TodoDto, TodoInsertDto, TodoUpdateDto>
{
    private readonly IRepository<ToDo> _todoRepository;
    private readonly IMapper _mapper;
    public List<string> Errors { get;}
    public TodoService(IRepository<ToDo> todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
        Errors = new List<string>();
    }
    
    public async Task<IEnumerable<TodoDto>> Get()
    {
        var todos = await _todoRepository.Get();

        return todos.Select(t => _mapper.Map<TodoDto>(t));
    }

    public async Task<TodoDto> GetById(int id)
    {
        var todo = await _todoRepository.GetById(id);
        
        if (todo != null)
        {
            var todoDto = _mapper.Map<TodoDto>(todo);
            
        return todoDto;
        }
        
        return null;
    }

    public async Task<TodoDto> Add(TodoInsertDto todoInsertDto)
    {
        var todo = _mapper.Map<ToDo>(todoInsertDto);

        await _todoRepository.Add(todo);
        await _todoRepository.Save();

        var todoDto = _mapper.Map<TodoDto>(todo);

        return todoDto;
    }
    public async Task<TodoDto> Update(int id, TodoUpdateDto todoUpdateDto)
    {
        var todo = await _todoRepository.GetById(id);
        if (todo != null)
        {
            todo = _mapper.Map<TodoUpdateDto, ToDo>(todoUpdateDto, todo);

             _todoRepository.Update(todo);
             await _todoRepository.Save();

             var todoDto = _mapper.Map<TodoDto>(todo);
             
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

            var todoDto = _mapper.Map<TodoDto>(todo);
        
        return todoDto;
        }
        
        return null;
    }

    public bool Validate(TodoInsertDto todoInsertDto)
    {
        if (_todoRepository.Search(b => b.ToDoName == todoInsertDto.ToDoName).Any())
        {
            Errors.Add("Ya existe una tarea con este nombre");
            return false;
        }
        return true;
    }
    
    public bool Validate(TodoUpdateDto todoUpdateDto)
    {
        if (_todoRepository.Search(b => b.ToDoName == todoUpdateDto.ToDoName
            && b.TodoId != todoUpdateDto.TodoId).Any())
        {
            Errors.Add("Ya existe una tarea con este nombre");
            return false;
        }
        return true;
    }
    
}