using src.Entities;

namespace src.Models.Dtos
{
    public class UpdatedRequestDTO
    {
        public string IsUpdated { get; set; }
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public DateTime TimeStamp { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ReviewString { get; set; }
        public StatusType Status { get; set; }
    }
}
