using System.ComponentModel.DataAnnotations;

namespace ELearning.Areas.Instructor.ViewModels
{
    public class EditSection_ViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
}