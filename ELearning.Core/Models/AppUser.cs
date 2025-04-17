using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Core.Models
{
	public class AppUser : IdentityUser<int>
	{
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public DateTime CreatedAt { get; set; }
		public string? Bio {  get; set; }
		public int? ImageId {  get; set; }
		public virtual Cart Cart { get; set; }
		public virtual Image Image { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = [];
		public virtual ICollection<Course> Courses { get; set; } = [];
    }
}