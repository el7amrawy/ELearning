using ELearning.Core.Models;

namespace ELearning.Areas.Instructor.ViewModels
{
    public class EditLecture_ViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Notes { get; set; }
        public IFormFile? VideoFile { get; set; }
        public Video? Video { get; set; }
    }
}