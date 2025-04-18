namespace ELearning.ViewModels
{
    public class Cart_ViewModel
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public List<CartItem_ViewModel> CartItems { get; set; }
    }
}