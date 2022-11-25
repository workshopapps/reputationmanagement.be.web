using Microsoft.IdentityModel.Tokens;
using src.Models;
using src.Models.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace src.CustomValidations
{
    public class EmailBodyValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var emailData = (EmailDataDto)validationContext.ObjectInstance;
            return isEmailBodyValid(emailData.EmailBody) ? ValidationResult.Success : new ValidationResult("Body formatting seems wrong, please check your text for errors in formatting");
        }

        public static bool isEmailBodyValid(string body)
        {
            var regex = "<(\"[^\"]*\"|'[^']*'|[^'\">])*>";

            if (body.IsNullOrEmpty())
            {
                return false;
            }
            if(Regex.IsMatch(body, regex) || body.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
