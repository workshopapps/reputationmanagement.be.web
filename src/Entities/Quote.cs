using System.ComponentModel.DataAnnotations;

namespace src.Entities
{
    public class Quote
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [EmailAddress]
        [Required(ErrorMessage = "Please We need a valid email address so that we may contact you")]
        public string Email { get; set; }

        [Phone]
        [Required(ErrorMessage = "Please We need a valid email address so that we may contact you")]
        public string Phone { get; set; }

        [Required]
        public string BusinessName { get; set; }
        [Required]
        public string FullName { get; set; }

        [Required]
        public string ReviewLocation { get; set; }

        public string? AboutTheReview { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = default;
        public DateTime LastAccessed { get; set; } = default;
    }
}