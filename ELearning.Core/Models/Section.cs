namespace ELearning.Core.Models
{
    public class Section
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Order { get; set; }
		public double Duration { get; set; }
		public int CourseId { get; set; }
		public Course Course { get; set; }
		public virtual ICollection<Lecture> Lectures { get; set; } = [];
	}
}