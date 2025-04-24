using ELearning.Core.Models;

namespace ELearning.ViewModels
{
    public class LearnLecture_ViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public int SectionId { get; set; }
        public Video Video { get; set; }
    }
}
