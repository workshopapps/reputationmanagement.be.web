namespace src.Models.Dtos
{
    public class NotificationContext
    {
        public string Address { get; set; } = string.Empty;
        public string Header { get; set; } = string.Empty;
        public string Purpose { get; set; } = null!;
    }
}
