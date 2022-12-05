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
    }

    public class LawyerReviewForUpdateDTO
    {
        [Required]
        public StatusType Status { get; set; }
    }
}
