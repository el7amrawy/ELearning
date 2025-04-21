namespace ELearning.ViewModels
{
    public class Enrollment_ViewModel
    {
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsPaid { get; set; }
        public CourseCard_ViewModel Course { get; set; }
    }
}