using System.ComponentModel.DataAnnotations;

namespace src.Models.Auth
{
    public class UserRegistrationModel
    {
        [Required]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
