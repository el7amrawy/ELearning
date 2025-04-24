namespace ELearning.ViewModels
{
    public class LearnSection_ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public List<LearnLecture_ViewModel> Lectures { get; set; }
    }
}