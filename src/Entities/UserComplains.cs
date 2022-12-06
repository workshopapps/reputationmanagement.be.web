using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace src.Entities
{
    public class UserComplains
    {
        [Key]
        public Guid ComplaintId { get; set; }
        
        public string UserId { get; set; }

        [Required]
        public string ComplaintMessage { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
