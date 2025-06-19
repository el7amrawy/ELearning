using System.ComponentModel.DataAnnotations;

namespace ELearning.ViewModels
{
    public class VerifyOTP_ViewModel
    {
        public string Email { get; set; }
        [MaxLength(6 ,ErrorMessage ="Length must be 6"),MinLength(6, ErrorMessage = "Length must be 6")]
        [Required(ErrorMessage = "OTP is required.")]
        public string OTP { get; set; }
    }
}