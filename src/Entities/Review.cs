
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace src.Entities
{
    public class Review
    {
        
        [Key]
        [Required]
        public Guid ReviewId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
       
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = default;
        public DateTime ViewLastTime { get; set; } = default;

        [Required]
        public string ReviewString { get; set; }
        [Required]
        public StatusType Status { get; set; }
        
        public PriorityType Priority { get; set; } = PriorityType.NotUrgent;
        
        public string? LawyerEmail { get; set; }

        ///new reqs
        [Required]
        public string BusinessType { get; set; }
        [Required]
        public string WebsiteName { get; set; }
        [Required]
        public int Rating { get; set; }

        /// new reqs 5/12/2022
        [Required]
        public string ComplainerName { get; set; } = "Ciroma Chukwuma Adekunle";

        // new req 12/8/2022
        [Required]
        public DateTime TimeOfReview { get; set; } = default;
    }

    public enum StatusType
    {
        pending,
        InProgress,
        inconclusive,
        completed,
        failed
    }
    public enum PriorityType
    {
        High,
        Low,
        Medium,
        NotUrgent
    }
}
