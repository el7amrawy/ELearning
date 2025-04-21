namespace ELearning.Core.DTOs
{
    public class CheckoutCourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int Quantity => 1;
    }
}