using src.Entities;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class ReviewCsvDto
    {
        public string Email { get; set; }
        public string ReviewString { get; set; }
        public string CustomerEmail { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
    }
}
