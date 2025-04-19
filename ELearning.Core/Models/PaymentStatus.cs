namespace ELearning.Core.Models
{
    public class PaymentStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Payment> Payments { get; set; } = [];
    }
}