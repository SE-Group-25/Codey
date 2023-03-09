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

        [Column("Title")]
        public string Title { get; set; }

        [Column("Tags")]
        public string Tags { get; set; }

        [Column("Url")]
        public string Url { get; set; }

        [Column("IsCostRequired")]
        public string IsCostRequired { get; set; }

        #endregion
    }
}
