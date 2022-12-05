using src.Entities;

namespace src.Models.Dtos
{
    public class ReviewForDisplayDto
    {
        public Guid ReviewId { get; set; }
        
        public string Email { get; set; }
        public string ReviewString { get; set; }
        public string BusinessType { get; set; }
        public string WebsiteName { get; set; }
        public int Rating { get; set; }
     
        public StatusType Status { get; set; }
        public PriorityType Priority { get; set; }

        //map to CreatedAt
        public DateTime TimeOfReview { get; set; }
        public DateTime LastUpdated { get; set; }



    }
}
