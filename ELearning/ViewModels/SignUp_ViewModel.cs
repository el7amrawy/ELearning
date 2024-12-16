using System.ComponentModel.DataAnnotations;

namespace ELearning.ViewModels
{
    public class SignUp_ViewModel
    {
        [Required,Display(Name ="First Name")]
        public string  FirstName { get; set; }
        [Required,Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
