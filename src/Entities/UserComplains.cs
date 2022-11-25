using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace src.Entities
{
    public class UserComplains
    {
        [Key]
        public Guid ComplaintId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public string ComplaintMessage { get; set; }

        public DateTime TimeStamp { get; set; }

        public ApplicationUser User { get; set; }
    }
}
