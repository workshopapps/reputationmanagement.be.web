using Microsoft.AspNetCore.Identity.UI.Services;
using src.Models;

namespace src.Helpers
{
    public static class IEmailSenderExtensionsHelper
    {

        public static void SendEmailWrapper(this IEmailSender emailSender, EmailData emailData)
        {
            
            emailSender.SendEmailAsync(emailData.EmailToId, emailData.EmailSubject, emailData.EmailBody);

        }
    }
}
