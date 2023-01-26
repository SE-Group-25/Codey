using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SE_AI_Skills_Tool.Models;

public class Course
{
    [Column("Id")]
    [Key]
    public int Id { get; set; }
    
    [Column("Name")]
    public string Name { get; set; }
    
    [Column("IsAvailable")]
    public bool IsAvailable { get; set; }
    
    [Column("WebAddress")]
    public string WebAddress { get; set; }
    
    [Column("Description")]
    public string Description { get; set; }
    
    [Column("Difficulty")]
    public string Difficulty { get; set; }
    
    [Column("Tags")]
    public string Tags { get; set; }
}
