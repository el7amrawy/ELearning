using System.ComponentModel.DataAnnotations;

namespace ELearning.ViewModels
{
    public class EditUserProfile_ViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        [Display(Name = "Phone Number"), Phone]
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
    }
}
