using System.Text.Json.Serialization;

namespace ELearning.Core.DTOs
{
    public class CheckoutCourse
    {
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonPropertyName("amount")]
        public int Amount { get; set; }
        public int Quantity => 1;
    }
}