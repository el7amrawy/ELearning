using ELearning.Core.Models;

namespace ELearning.Core.DTOs
{
    public class PaymentRequest
    {
        public string UserId { get; set; }
        public decimal Amount { get; set; }
        public string BillingEmail { get; set; }
        public string BillingFirstName { get; set; }
        public string BillingLastName { get; set; }
        public string Currency { get; set; } = "EGP";
        public List<Course> Courses { get; set; } = [];
    }
}