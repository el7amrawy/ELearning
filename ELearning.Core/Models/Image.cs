using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
    [Table("Images")]
    public class Image
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string URL { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
