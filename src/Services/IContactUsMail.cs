namespace src.Services
{
    // Contact us interface
    public interface IContactUsMail
    {
        Task SendEmailAsync(string fromEmail, string subject, string htmlMessage);
    }
}
