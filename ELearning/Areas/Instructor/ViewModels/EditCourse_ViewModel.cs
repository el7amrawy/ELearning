using ELearning.Core.Models;

namespace ELearning.Areas.Instructor.ViewModels
{
    public class EditCourse_ViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int LanguageId { get; set; }
        public int LevelId { get; set; }
        public int CategoryId { get; set; }
        public IFormFile? ImageFile { get; set; }
        public Image? Image { get; set; }
    }
}