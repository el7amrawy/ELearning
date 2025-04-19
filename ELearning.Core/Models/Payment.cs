using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
    [Table("Payments")]
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public string StripePaymentIntentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual AppUser User { get; set; }
        public virtual Course Course { get; set; }
        public virtual PaymentStatus Status { get; set; }
    }
}