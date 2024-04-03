using Microsoft.EntityFrameworkCore;

namespace TodoList.Models;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    { }
    
    public DbSet<ToDo>? ToDos { get; set; }
    public DbSet<User>? Users { get; set; }
}