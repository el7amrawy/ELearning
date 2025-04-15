using ELearning.Core.Models;

namespace ELearning.ViewModels
{
    public class CourseCard_ViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public decimal Price { get; set; }
        public double Duration { get; set; }
        public string LevelName { get; set; }
        public CourseCardInstructor_ViewModel Instructor { get; set; }
        public virtual Image Image { get; set; }
    }
}