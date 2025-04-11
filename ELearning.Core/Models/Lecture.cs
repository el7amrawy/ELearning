namespace ELearning.Core.Models
{
    public class Lecture
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string? Notes { get; set; }
		public int Order {  get; set; }
		public int SectionId { get; set; }
		public int VideoId { get; set; }
		public DateTime CreatedAt { get; set; }
		public Video? Video { get; set; }
		public Section? Section { get; set; }
		public Material? Material { get; set; }
	}
}