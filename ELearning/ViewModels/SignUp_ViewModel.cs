using System.ComponentModel.DataAnnotations;

namespace ELearning.ViewModels
{
    public class SignUp_ViewModel
    {
        [Required,Display(Name ="First Name")]
        public string  FirstName { get; set; }
        [Required,Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Username {  get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$#!%*?&]{8,}$",
                ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character(@$#!%*?&).")]
        public string Password { get; set; }
    }
}
