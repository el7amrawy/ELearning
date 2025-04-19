using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Core.Models
{
    [Table("Checkouts"),PrimaryKey(nameof(UserId),nameof(CourseId))]
    public class Checkout
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public long Amount { get; set; } // In cents
        public string Currency { get; set; } = "USD";
        public virtual Course Course { get; set; }
        public virtual AppUser User { get; set; }
    }
}