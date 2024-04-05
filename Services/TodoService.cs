using AutoMapper;
using TodoList.DTOs;
using TodoList.Models;
using TodoList.Repository;

namespace TodoList.Services;

public class TodoService : ICommonService<TodoDto, TodoInsertDto, TodoUpdateDto>
{
    private readonly IRepository<ToDo> _todoRepository;
    private readonly IMapper _mapper;
    public TodoService(IRepository<ToDo> todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
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
}