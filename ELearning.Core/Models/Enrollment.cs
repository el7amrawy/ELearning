using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
	[PrimaryKey(nameof(StudentId),nameof(CourseId))]
	public class Enrollment
	{
		[Column(Order =0)]
		public int StudentId { get; set; }
		[Column(Order = 1)]
		public int CourseId { get; set; }
		[Required]
		public DateTime EnrollmentDate {  get; set; }
		public Student? Student { get; set; }
		public Course? Course { get; set; }
	}
}
