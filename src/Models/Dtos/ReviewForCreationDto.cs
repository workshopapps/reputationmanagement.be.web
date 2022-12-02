using src.Entities;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class ReviewForCreationDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ReviewString { get; set; }
        [Required]
        public StatusType Status { get; set; }
        [Required]
        public PriorityType Priority { get; set; }
    }
}
