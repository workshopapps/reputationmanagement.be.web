

using MailKit.Security;
using src.CustomValidations;
namespace src.Models
{
    public class MailKitEmailSenderOptions
    {
        public string Host_Address { get; set; }

        public int Host_Port { get; set; }

        public string Host_Username { get; set; }

        public string Host_Password { get; set; }

        public SecureSocketOptions Host_SecureSocketOptions = SecureSocketOptions.Auto;

        [EmailValidator]
        public string Sender_Email { get; set; }

        public string Sender_Name { get; set; }
    }
}
