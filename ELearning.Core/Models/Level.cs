using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
    [Table("Levels")]
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
