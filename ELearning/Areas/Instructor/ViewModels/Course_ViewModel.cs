using ELearning.Core.Models;
using ELearning.Core.Enums;

namespace ELearning.Areas.Instructor.ViewModels
{
    public class Course_ViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public CourseStatusEnum StatusId { get; set; }
        public string StatusName { get => StatusId.ToString(); }
        public int StudentsNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Image Image { get; set; }
    }
}