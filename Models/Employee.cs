using System.ComponentModel.DataAnnotations;

namespace EMS.Models;

public class Employee
{
    [Key] // Marks this as the primary key
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
