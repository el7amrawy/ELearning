using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
	public class Section
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = null!;
		[Required,ForeignKey(nameof(Course))]
		public int CourseId { get; set; }
		public Course? Course { get; set; }
		public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
	}
}
