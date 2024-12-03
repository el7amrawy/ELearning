using System.ComponentModel.DataAnnotations;

namespace ELearning.Core.Models
{
	public class Course
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; } = null!;
		[Required]
		public string Description { get; set; } = null!;
		[Required]
		public decimal Price { get; set; }
		[Required]
		public string Cover { get; set; } = null!;
		[Required]
		public DateTime CreatedAt { get; set; }
		public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
		public virtual ICollection<Section> Sections { get; set; } = new List<Section>();
		public virtual ICollection<Student> Students { get; set; } = new List<Student>();
	}
}
