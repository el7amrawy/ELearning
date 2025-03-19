using System.ComponentModel.DataAnnotations;

namespace ELearning.Areas.Dashboard.ViewModels
{
    public class ResetPassword_ViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name ="Old Password")]
        public string OldPassword { get; set; }
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string NewPassword { get; set; }
        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword),ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
