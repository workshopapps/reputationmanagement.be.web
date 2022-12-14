using src.Entities;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class ReviewForCreationDto
    {
        public string Email { get; set; }
        public string ReviewString { get; set; }
        public PriorityType Priority { get; set; }
        public string BusinessType { get; set; } = "Online Store";
        public string WebsiteName { get; set; } = "Ankers";
        public int Rating { get; set; }
        public string ReviewLink { get; set; } = "";
        public string ComplainerName { get; set; } = "Ciroma Chukwuma Adekunle";
        public DateTime TimeOfReview { get; set; } = default;

    }
}
