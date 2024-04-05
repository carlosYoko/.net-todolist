using AutoMapper;
using TodoList.DTOs;
using TodoList.Models;

namespace TodoList.Automappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoInsertDto, ToDo>();
        
        CreateMap<ToDo, TodoDto>()
            .ForMember(dto => dto.TodoId, 
                m => m.MapFrom(
                    t => t.TodoId));

        CreateMap<TodoUpdateDto, ToDo>();
    }
}