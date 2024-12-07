using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required,ForeignKey(nameof(User))]
        public int UserId { get; set; }
        [Required]
        public decimal Total { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }= new List<CartItem>();
    }
}
