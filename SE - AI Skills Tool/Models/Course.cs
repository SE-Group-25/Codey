using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SE_AI_Skills_Tool.Models
{
    [Table("Courses")]
    public class Course
    {
        #region Properties

        [Column("Description")]
        public string Description { get; set; }

        [Column("Difficulty")]
        public string Difficulty { get; set; }

        [Column("Id")]
        [Key]
        public int Id { get; set; }

        [Column("IsAvailable")]
        public bool IsAvailable { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Tags")]
        public string Tags { get; set; }

        [Column("WebAddress")]
        public string WebAddress { get; set; }

        #endregion
    }
}
