namespace ELearning.ViewModels
{
    public class LearnCourse_ViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public CourseDetailsInstructor_ViewModel Instructor { get; set; }
        public List<LearnSection_ViewModel> Sections { get; set; }
    }
}