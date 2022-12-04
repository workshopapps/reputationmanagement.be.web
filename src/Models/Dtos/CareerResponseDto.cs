using src.Entities;
using System.ComponentModel.DataAnnotations;
using src.CustomValidations;

namespace src.Models.Dtos
{
    public class CareerResponseDto
    {
        [Required]
        public string FIrstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public IFormFile Resume { get; set; }
        [Required]
        public IFormFile? CoverLetter { get; set; }
    }
}
