using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class CustomerAccountForCreationDto
    {
        public CustomerAccountForCreationDto()
        {
            Roles = new List<string>();
        }

        [Required]
        public string BusinessEntityName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IList<string> Roles { get; set; }
    }
}
