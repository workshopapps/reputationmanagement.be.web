using src.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models.Dtos
{
    public class DisputeForCreationDto
    {
        [Required]
        public string ReviewId { get; set; }
        [Required]
        public string Complaint { get; set; }
        [Required]
        public string ComplaintMessage { get; set; }
        [Required]
        public Reasons Reason { get; set; }       
    }
}
