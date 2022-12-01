
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
        public DateTime UpdatedAt { get; set; }
        public DateTime ViewLastTime { get; set; }
        [Required]
        public string ReviewString { get; set; }
        [Required]
        public StatusType Status { get; set; }
        
        public PriorityType Priority { get; set; } = PriorityType.NotUrgent;
    }

    public enum StatusType
    {
        PendingReview,
        Successful,
        Inconclusive, 
        Failed
    }
    public enum PriorityType
    {
        High,
        Low,
        Medium,
        NotUrgent
    }
}
