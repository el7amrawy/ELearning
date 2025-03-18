using System.ComponentModel.DataAnnotations;

namespace ELearning.Areas.Dashboard.ViewModels
{
    public class SignIn_ViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(8,ErrorMessage ="password could not be less than 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}