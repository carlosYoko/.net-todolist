using TodoList.DTOs;

namespace TodoList.Services;

public interface ICommonService<T, TI, TU>
{
    Task<IEnumerable<T>> Get();
    Task<T> GetById(int id);
    Task<T> Add(TI todoInsertDto);
    Task<T> Update(int id, TU todoUpdateDto);
    Task<T> Delete(int id);
}