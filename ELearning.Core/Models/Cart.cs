namespace ELearning.Core.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual AppUser? User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; } = [];
    }
}