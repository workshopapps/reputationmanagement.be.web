using src.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class EmailDataDto
    {
        [EmailValidator]
        public string EmailToId { get; set; }

        [EmailBodyValidator]
        public string EmailBody { get; set; }
    }
}
