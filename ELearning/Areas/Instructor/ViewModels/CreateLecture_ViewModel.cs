using System.ComponentModel.DataAnnotations;

namespace ELearning.Areas.Instructor.ViewModels
{
    public class CreateLecture_ViewModel
    {
        public string Title { get; set; }
        public string? Notes { get; set; }
        [Display(Name ="Video")]
        public IFormFile VideoFile {  get; set; }
    }
}