using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SE_AI_Skills_Tool.Models
{
    [Table("Users")]
    public class User
    {
        
        // ToDo: only have Id and courses use ibm login for the user ids use this to link between courses and users.
        
        [Column("Id")]
        [Key]
        public string Id { get; set; }

        [Column("FirstName")]
        public string? FirstName { get; set; }

        [Column("LastName")]
        public string? LastName { get; set; }

        [Column("Courses")]
        public Course[]? Courses { get; set; }
        
        [Column("SessionId")]
        public string? SessionId { get; set; }
    }
}
