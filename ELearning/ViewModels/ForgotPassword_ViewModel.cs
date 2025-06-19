using System.ComponentModel.DataAnnotations;

namespace ELearning.ViewModels
{
    public class ForgotPassword_ViewModel
    {
        [EmailAddress(ErrorMessage ="Email is not valid"),Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
    }
}