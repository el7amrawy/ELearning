namespace ELearning.Areas.Instructor.ViewModels
{
    public class HomeIndex_ViewModel
    {
        public InstructorHomeIndex_ViewModel Instructor { get; set; }
        public IEnumerable<Course_ViewModel> Courses { get; set; }
    }
}