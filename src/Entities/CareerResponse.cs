using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace src.Entities
{
    public class CareerResponse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]  
        public string PhoneNumber { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string ResumeFileName { get; set; }
        [Required]
        public string CoverLetterFileName { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}
