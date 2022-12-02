using src.Entities;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class ChallengeUserReviewDto
    {
        [Key]
        [Required]
        public Guid ReviewId { get; set; }

        public Guid UserId { get; set; }

        public string ComplaintMessage { get; set; }

        public StatusType Status { get; set; }
    }
}
