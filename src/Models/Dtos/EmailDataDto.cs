using src.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class EmailDataDto
    {
        [Required]
        [EmailValidator]
        public string EmailToId { get; set; }

        [Required]
        [EmailBodyValidator]
        public string EmailBody { get; set; }
    }
}


