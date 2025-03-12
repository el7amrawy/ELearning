using System.ComponentModel.DataAnnotations;
using ELearning.Core.Models;

namespace ELearning.ViewModels
{
    public class UserProfile_ViewModel
    {
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Phone Number"),Phone,Range(12,12,ErrorMessage ="Required length is 12")]
        public string? PhoneNumber { get; set; }
        public string? Bio { get; set; }
        public Image? Image { get; set; }
    }
}