using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace src.Entities
{
    public class Dispute
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();   
        [Required]
        public string UserId { get; set; }
        [Required]
        public string ReviewId{ get; set; }
        public string? LawyerEmail { get; set; } 
        [Required]
        public string Complaint { get; set; }
        [Required]
        public string ComplaintMessage { get; set; }

        public string BadReviewerEmail { get; set; } = "";
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        public Reasons Reason { get; set; } = Reasons.Unresolved;
        public DisputeStatus Status { get; set; }
    }

    public enum Reasons
    {
        Unresolved, DelayedRequest, Other
    }
    public enum DisputeStatus
    {
        Opened,
        Closed
    }

}
