using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class CustomerUpdateDto
    {
        [Required]
        public string BusinessEntityName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? BusinessWebsite { get; set; }
        public string? BusinessDescription { get; set; }
    }
}
