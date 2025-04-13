using ELearning.Helpers;

namespace ELearning.Areas.Instructor.ViewModels
{
    public class CoursesIndex_ViewModel
    {
        public IEnumerable<Course_ViewModel> Courses { get; set; }
        public Pagination Pagination { get; set; }
    }
}