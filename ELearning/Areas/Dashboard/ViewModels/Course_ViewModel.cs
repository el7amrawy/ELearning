using ELearning.Core.Enums;
using ELearning.Core.Models;
using ELearning.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace ELearning.Areas.Dashboard.ViewModels
{
    public class Course_ViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public decimal Price { get; set; }
        public double Duration { get; set; }
        [Display(Name = "Status")]
        public CourseStatusEnum StatusId { get; set; }
        public string StatusName { get => StatusId.ToString(); }
        [Display(Name = "Created")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Updated")]
        public DateTime UpdatedAt { get; set; }
        public string Image { get; set; }
        public Language Language { get; set; }
        public Level Level { get; set; }
        public Category_ViewModel Category { get; set; }
        public CourseDetailsInstructor_ViewModel Instructor { get; set; }
    }
}
