namespace ELearning.ViewModels
{
    public class CartItem_ViewModel
    {
        public int CartId { get; set; }
        public DateTime CreatedAt { get; set; }
        public CartCourse_ViewModel Course { get; set; }
    }
}
