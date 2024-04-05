using TodoList.DTOs;

namespace TodoList.Services;

public interface ICommonService<T, TI, TU>
{
    public List<string> Errors { get; }
    Task<IEnumerable<T>> Get();
    Task<T> GetById(int id);
    Task<T> Add(TI todoInsertDto);
    Task<T> Update(int id, TU todoUpdateDto);
    Task<T> Delete(int id);
    bool Validate(TI dto);
    bool Validate(TU dto);
}