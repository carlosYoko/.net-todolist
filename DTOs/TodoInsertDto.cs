namespace TodoList.DTOs;

public class TodoInsertDto
{
    public string? ToDoName { get; set; }
    
    public bool IsDone { get; set; }
    
    public int UserId { get; set; }
}