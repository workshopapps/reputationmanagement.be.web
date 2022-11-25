using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;
using src.Models;
using System.Net.Mail;
using src.Models.Dtos;
using System.Text.RegularExpressions;

namespace src.CustomValidations
{
    public class EmailValidator: ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {

            var emailData = (EmailDataDto)validationContext.ObjectInstance;
            if (emailData.EmailToId.IsNullOrEmpty() || !isEmailValid(emailData.EmailToId))
            {
                return new ValidationResult("Invalid email format");
            }
            return ValidationResult.Success;
        }

        public static bool isEmailValid(string email) {

            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
            return Regex.IsMatch(email, regex);
        }
    }
}
