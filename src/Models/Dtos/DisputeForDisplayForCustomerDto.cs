using src.Entities;
using System.ComponentModel;

namespace src.Models.Dtos
{
    public class DisputeForDisplayForCustomerDto
    {
        public string Id { get; set; }
        public string ReviewId { get; set; }
        public string Complaint { get; set; }
        [DisplayName("CreatedAt")]
        public DateTime TimeStamp { get; set; }
        public Reasons Reason { get; set; } = Reasons.Unresolved;
        public DisputeStatus Status { get; set; } = DisputeStatus.Opened;
    }
}