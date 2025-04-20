namespace ELearning.Core.Models
{
    public class Course
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string SubTitle { get; set; }
        public string Description { get; set; }
		public decimal Price { get; set; }
		public double Duration { get; set; }
		public int LanguageId { get; set; }
		public int LevelId { get; set; }
		public int StatusId { get; set; }
		public int CategoryId { get; set; }
		public int ImageId { get; set; }
        public DateTime CreatedAt {  get; set; }
		public DateTime UpdatedAt { get; set; }
		public virtual Image Image { get; set; }
		public virtual Language Language { get; set; }
		public virtual Level Level { get; set; }
		public virtual CourseStatus Status { get; set; }
		public virtual Category? Category { get; set; }
        public virtual ICollection<AppUser> Instructors { get; set; } = [];
        public virtual ICollection<Section> Sections { get; set; } = [];
        public virtual ICollection<Enrollment> Enrollments { get; set; } = [];
    }
}