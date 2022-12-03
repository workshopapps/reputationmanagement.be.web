
using FluentValidation;

namespace src.Models.Dtos
{
    public class ResetPasswordDto
    {
        public string Email { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;


        public string NewPassword { get; set; } = string.Empty;


        public string ConfirmPassword { get; set; } = string.Empty;
    }
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordValidator()
        {
            RuleFor(ResetPasswordDto => ResetPasswordDto.Email).EmailAddress().WithMessage("Invalid Email Address").NotEmpty().NotNull();
            RuleFor(ResetPasswordDto => ResetPasswordDto.Token).NotEmpty().NotNull();

            RuleFor(ResetPasswordDto => ResetPasswordDto.NewPassword).NotEmpty().NotNull().WithMessage("Password cannot be empty");
            RuleFor(ResetPasswordDto => ResetPasswordDto.ConfirmPassword.Equals(ResetPasswordDto.NewPassword)).NotEmpty().WithMessage("Password must match");
        }
    }
}
