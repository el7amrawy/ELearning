namespace ELearning.Areas.Instructor.ViewModels
{
    public class CreateCourse_ViewModel
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int LanguageId { get; set; }
        public int LevelId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedAt { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}