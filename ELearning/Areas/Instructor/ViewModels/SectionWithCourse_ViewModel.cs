using ELearning.Core.Models;

namespace ELearning.Areas.Instructor.ViewModels
{
    public class SectionWithCourse_ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SectionCourse_ViewModel Course { get; set; }
        public ICollection<Lecture> Lectures { get; set; } = [];
    }
}