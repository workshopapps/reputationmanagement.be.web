using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class QuoteForCreationFromBlogDto
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string BusinessName { get; set; }
        public string ReviewLocation { get; set; }
        public string FullName { get; set; }
        [Required]
        public string AboutTheReview { get; set; }
    }
}
