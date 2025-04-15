using System.ComponentModel.DataAnnotations;

namespace ELearning.Core.Models
{
    public class Section
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public int Order { get; set; }
		public double Duration { get; set; }
		public int CourseId { get; set; }
		public Course? Course { get; set; }
		public virtual ICollection<Lecture> Lectures { get; set; } = [];
	}
}