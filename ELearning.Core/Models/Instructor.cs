namespace ELearning.Core.Models
{
	public class Instructor:User 
	{
		public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
	}
}
