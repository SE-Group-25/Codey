using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SE_AI_Skills_Tool.Models
{
    [Table("Users")]
    public class User
    {
        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("CreatedOn")]
        public DateTimeOffset? CreatedOn { get; set; }

        [Column("Courses")]
        public int Courses { get; set; }
    }
}
