using Microsoft.EntityFrameworkCore;
using TodoList.Models;

namespace TodoList.Repository;

public class TodoRepository : IRepository<ToDo>
{
    private readonly StoreContext _context;

    public TodoRepository(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ToDo>> Get()
    {
        return await _context.ToDos!.ToListAsync();
    }

    public async Task<ToDo> GetById(int id)
    {
        var result =await _context.ToDos!.FindAsync(id);
        return result ?? new ToDo();
    }

    public async Task Add(ToDo todo)
    {
         await _context.ToDos!.AddAsync(todo);
    }

    public void Update(ToDo todo)
    {
        _context.ToDos!.Attach(todo);
        _context.ToDos!.Entry(todo).State = EntityState.Modified;
    }

    public void Delete(ToDo todo)
    {
        _context.ToDos!.Remove(todo);
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}