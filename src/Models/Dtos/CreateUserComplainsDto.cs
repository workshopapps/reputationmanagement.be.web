using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models.Dtos
{
    public class CreateUserComplainsDto
    {
        public Guid UserId { get; set; }

        [Required]
        public string ComplaintMessage { get; set; }
    }
}
