namespace ELearning.Core.Models
{
	public class Student:User
	{
		public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
		public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
	}
}
