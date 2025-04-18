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
        public DateTime CreatedAt { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual Course? Course { get; set; }
    }
}
