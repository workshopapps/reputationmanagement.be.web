namespace src.Models.Dtos
{
    public class UpdateNotificationForUserDto
    {
        public bool ComplaintStatus { get; set; } = true;
        public bool InvoiceReceipt { get; private set; } = true;
    }
}
