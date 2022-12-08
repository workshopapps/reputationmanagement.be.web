using src.Entities;
using System.ComponentModel.DataAnnotations;
using src.CustomValidations;

namespace src.Models.Dtos
{
    public class CareerResponseDto
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string position { get; set; }
        [Required]
        public string reason { get; set; }
        [Required]
        public IFormFile resume { get; set; }
        [Required]
        public IFormFile? coverLetter { get; set; }
    }
}
