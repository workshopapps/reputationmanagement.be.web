using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using src.Models;

namespace src.Services
{
    public class ContactUsMailKit : IContactUsMail
    {
        public MailKitEmailSenderOptions Options { get; set; }
        public ContactUsMailKit(IOptions<MailKitEmailSenderOptions> options)
        {
            this.Options = options.Value;
        }

        //public EmailData EmailData { get; set; }


        public Task SendEmailAsync(string fromEmail, string subject, string message)
        {
            return Execute(fromEmail, subject, message);
        }

        public Task Execute(string fromEmail, string subject, string message)
        {
            string toEmail = "davidokeke.c@gmail.com";
            try
            {
                // create message
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(fromEmail);
                if (!string.IsNullOrEmpty(fromEmail))
                    email.Sender.Name = Options.Sender_Name;
                email.From.Add(email.Sender);
                email.To.Add(MailboxAddress.Parse(toEmail));
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
                catch (SmtpCommandException ex)
                { throw ex; }

                return Task.FromResult(true);
            }
            catch { throw; }
        }
    }
}
