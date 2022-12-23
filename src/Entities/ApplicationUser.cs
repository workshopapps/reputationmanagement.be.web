using Microsoft.AspNetCore.Identity;

namespace src.Entities
{
    public class ApplicationUser:IdentityUser
    {

        public string PostAddress { get; set; }
        public string Language { get; set; } = "english";
        public bool LargeText { get; set; }
        public bool ScreenReader { get; set; }
        public bool HighContrast { get; set; }

        // Notificaton Settings
        public bool ComplaintStatus { get; set; } = true;
        public bool InvoiceReceipt { get; set; } = true;

        // More Identity
        public string? FullName { get; set; }
        public string? BusinessWebsite { get; set; }
        public string? BusinessDescription { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
