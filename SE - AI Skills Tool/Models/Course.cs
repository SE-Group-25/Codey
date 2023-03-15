using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SE_AI_Skills_Tool.Models
{
    [Table("Courses")]
    public class Course
    {
        #region Properties

        [Column("Description")]
        public string? Description { get; set; }

        [Column("Importance")]
        public string? Importance { get; set; }

        [Column("Id")]
        [Key]
        public string Id { get; set; }

        [Column("Keywords")]
        public string? Keywords { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("Tags")]
        public string? Tags { get; set; }

        [Column("Url")]
        public string Url { get; set; }
        
        [Column("IconUrl")]
        public string? IconUrl { get; set; }

        [Column("IsCostRequired")]
        public bool IsCostRequired { get; set; }
        
        //public User? User { get; set; }
        
        public string? UserId { get; set; }

        #endregion
    }
}
