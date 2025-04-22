using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; }
        public int StatusId { get; set; }
        public string Currency { get; set; } = "EGP";
        public string BillingEmail { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public virtual PaymentStatus Status { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [NotMapped]
        public int UserId => Enrollments.FirstOrDefault().StudentId;
        [NotMapped]
        public AppUser User => Enrollments.FirstOrDefault().Student;
    }
}