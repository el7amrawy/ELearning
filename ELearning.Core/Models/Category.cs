using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        [Display(Name = "Created")]
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = [];
    }
}