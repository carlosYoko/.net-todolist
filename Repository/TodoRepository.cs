using TodoList.Models;

namespace TodoList.Repository;

public class TodoRespository : IRepository<ToDo>
{
    public async Task<IEnumerable<ToDo>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ToDo>> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Add(ToDo entity)
    {
        throw new NotImplementedException();
    }

    public void Update(ToDo entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(ToDo entity)
    {
        throw new NotImplementedException();
    }

    public Task Save()
    {
        throw new NotImplementedException();
    }
}