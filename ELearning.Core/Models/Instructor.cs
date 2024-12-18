namespace ELearning.Core.Models
{
	public class Instructor: AppUser
    {
		public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
	}
}
