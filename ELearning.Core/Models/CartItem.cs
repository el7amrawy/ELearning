using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Core.Models
{
    [PrimaryKey(nameof(CartId),nameof(CourseId))]
    public class CartItem
    {
        [Column(Order = 0)]
        public int CartId {  get; set; }
        [Column(Order = 1)]
        public int CourseId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual Course? Course { get; set; }

    }
}
