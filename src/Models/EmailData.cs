using System.ComponentModel.DataAnnotations;
using src.CustomValidations;
namespace src.Models
{
    public class EmailData
    {
        [EmailValidator]
        public string EmailToId { get; set; }
        public string EmailSubject { get; set; }
        [EmailBodyValidator]
        public string EmailBody { get; set; }
    }
}
