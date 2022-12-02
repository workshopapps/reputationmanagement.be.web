using System.ComponentModel.DataAnnotations;

namespace src.Entities
{
    public class ChallengeReview
    {
        [Key]
        [Required]
        public Guid ReviewId { get; set; }

        public Guid UserId { get; set; }

        public string ComplaintMessage { get; set; }
    }
}
