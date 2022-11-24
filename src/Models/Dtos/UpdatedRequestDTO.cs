using src.Entities;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class UpdatedRequestDTO
    {
        public string TimeSinceUpdate { get; set; }
        public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ReviewString { get; set; }
        public StatusType Status { get; set; }
    }
}
