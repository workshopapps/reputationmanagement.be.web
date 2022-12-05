using src.CustomValidations;

namespace src.Models.Dtos
{
    public class ContactUsEmailDto
    {
        public string FromEmail { get; set; }

        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }
}
