using Microsoft.EntityFrameworkCore;
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
		public DateTime EnrollmentDate {  get; set; }
		public bool IsPaid {  get; set; }
		public int? PaymentId { get; set; }
		public Payment Payment { get; set; }
		public AppUser Student { get; set; }
		public Course Course { get; set; }
	}
}