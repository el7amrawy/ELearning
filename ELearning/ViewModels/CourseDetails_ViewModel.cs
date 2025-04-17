using System.ComponentModel.DataAnnotations;
using ELearning.Core.Models;

namespace ELearning.ViewModels
{
    public class CourseDetails_ViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Duration { get; set; }
        public int StatusId { get; set; }
        [Display(Name ="Created")]
        public DateTime CreatedAt { get; set; }
        [Display(Name ="Updated")]
        public DateTime UpdatedAt { get; set; }
        public string Image { get; set; }
        public Language Language { get; set; }
        public Level Level { get; set; }
        public Category_ViewModel Category { get; set; }
        public CourseDetailsInstructor_ViewModel Instructor { get; set; }
        public List<Section_ViewModel> Sections { get; set; }
    }
}