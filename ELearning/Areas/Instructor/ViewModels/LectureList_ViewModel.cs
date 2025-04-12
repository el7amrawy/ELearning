using ELearning.Core.Models;

namespace ELearning.Areas.Instructor.ViewModels
{
    public class LectureList_ViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public Video Video { get; set; }
        public Material Material { get; set; }
    }
}