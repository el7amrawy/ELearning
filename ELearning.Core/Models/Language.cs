using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
    [Table("Languages")]
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}