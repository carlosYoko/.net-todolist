using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoList.Models;

public class ToDo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TodoId { get; set; }
    
    [StringLength(50)]
    public string? ToDoName { get; set; }
    
    public bool IsDone { get; set; }
    
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}