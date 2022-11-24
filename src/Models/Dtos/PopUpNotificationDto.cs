using src.Entities;

namespace src.Models.Dtos
{
    public class PopUpNotificationDto
    {
        public string Email { get; set; }
        public StatusType Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
