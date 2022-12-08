using src.Entities;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class ReviewForUpdateDTO
    {
        public string ReviewString { get; set; }
        public StatusType Status { get; set; }
        public PriorityType Priority { get; set; }

        ///new reqs
        public string BusinessType { get; set; }
        public string WebsiteName { get; set; } 
        public int Rating { get; set; }
        public string Email { get; set; }

        /// new reqs 5/12/2022
        public string ComplainerName { get; set; } = "Ciroma Chukwuma Adekunle";
        public DateTime TimeOfReview { get; set; }
    }

    public class LawyerReviewForUpdateDTO
    {
        public StatusType Status { get; set; }
    }
}