using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearning.Core.Models
{
    public class Course
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string SubTitle { get; set; }
        [Required]
        public string Description { get; set; }
		[Required]
		public decimal Price { get; set; }
		public string? Time { get; set; }
		public int LanguageId { get; set; }
		[Required,ForeignKey(nameof(Level))]
		public int LevelId { get; set; }
		[Required,ForeignKey(nameof(Status))]
		public int StatusId { get; set; }
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
        [Required]
        public DateTime CreatedAt {  get; set; }
		public DateTime UpdatedAt { get; set; }
        public virtual ICollection<AppUser> Instructors { get; set; } = [];
		public virtual ICollection<Section> Sections { get; set; } = [];
		public virtual ICollection<Enrollment> Enrollments { get; set; } = [];
		public Image Image { get; set; }
		public virtual Language? Language { get; set; }
		public virtual Level? Level { get; set; }
		public virtual CourseStatus? Status { get; set; }
		public virtual Category? Category { get; set; }
	}
}