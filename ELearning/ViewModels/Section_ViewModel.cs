namespace ELearning.ViewModels
{
    public class Section_ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public virtual List<Lecture_ViewModel> Lectures { get; set; }
    }
}