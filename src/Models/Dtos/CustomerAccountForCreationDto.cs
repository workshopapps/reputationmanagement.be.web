using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class CustomerAccountForCreationDto
    {   
        [Required]
        public string BusinessEntityName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? FullName { get; set; }
        public string? BusinessWebsite { get; set; }
        public string? BusinessDescription { get; set; }
    }
}
