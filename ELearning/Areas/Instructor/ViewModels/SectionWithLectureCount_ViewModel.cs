namespace ELearning.Areas.Instructor.ViewModels
{
    public class SectionWithLectureCount_ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public double Duration { get; set; }
        public int LectureCount { get; set; }
    }
}