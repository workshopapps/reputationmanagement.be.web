using src.Entities;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class ReviewForUpdateDTO
    {
        [Required]
        public string ReviewString { get; set; } 
        [Required]
        public StatusType Status { get; set; }
        [Required]
        public PriorityType Priority { get; set; }

        ///new reqs
        [Required]
        public string BusinessType { get; set; }
        [Required]
        public string WebsiteName { get; set; }
        [Required]
        public int Rating { get; set; }

        [Required]
        public string Email { get; set; }

        /// new reqs 5/12/2022
        [Required]
        public string ComplainerName { get; set; } = "Ciroma Chukwuma Adekunle";
    }

    public class LawyerReviewForUpdateDTO
    {
        [Required]
        public StatusType Status { get; set; }
    }
}
