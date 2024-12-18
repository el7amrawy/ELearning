using System.ComponentModel.DataAnnotations;

namespace ELearning.ViewModels
{
    public class SignIn_ViewModel
    {
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
