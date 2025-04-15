namespace ELearning.Core.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string URL { get; set; }
        public double Duration { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}