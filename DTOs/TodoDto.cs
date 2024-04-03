namespace TodoList.DTOs;

public class TodoDto
{
    public int TodoId { get; set; }
    
    public string? ToDoName { get; set; }
    
    public bool IsDone { get; set; }
    
    public int UserId { get; set; }
}