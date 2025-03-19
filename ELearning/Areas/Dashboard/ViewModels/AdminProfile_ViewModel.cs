using System.ComponentModel.DataAnnotations;
using ELearning.Core.Models;

namespace ELearning.Areas.Dashboard.ViewModels
{
    public class AdminProfile_ViewModel
    {
        [Required, Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public IFormFile? Photo { get; set; }
        public Image? Image { get; set; }
    }
}