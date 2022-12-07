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

        //New reqs
        [Required]
        public string BusinessType { get; set; } = "Online Store";
        [Required]
        public string WebsiteName { get; set; } = "https://google.com";
        [Required]
        public int Rating { get; set; }

        /// new reqs 5/12/2022
        [Required]
        public string ComplainerName { get; set; } = "Ciroma Chukwuma Adekunle";
    }
}
