
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using src.Models;
using System.Threading.Tasks;

namespace src.Services
{

    public class MailKitEmailSender : IEmailSender
    {
        public MailKitEmailSenderOptions Options { get; set; }
        public MailKitEmailSender(IOptions<MailKitEmailSenderOptions> options)
        {
            this.Options = options.Value;
        }

        public EmailData EmailData { get; set; }
       
        
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(email, subject, message);
        }

        public Task Execute(string to, string subject, string message)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(Options.Sender_Email);
                if (!string.IsNullOrEmpty(Options.Sender_Name))
                    email.Sender.Name = Options.Sender_Name;
                email.From.Add(email.Sender);
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = message };

                try
                {
                    // send email
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Connect(Options.Host_Address, Options.Host_Port, Options.Host_SecureSocketOptions);
                        smtp.Authenticate(Options.Sender_Email, Options.Host_Password);
                        smtp.Send(email);
                        smtp.Disconnect(true);
                    }
                }
                catch(SmtpCommandException ex) 
                { throw ex; }

                return Task.FromResult(true);
            }
            catch { throw; }
        }
    }
}
