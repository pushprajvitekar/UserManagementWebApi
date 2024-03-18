using FluentValidation;
using UserManagement.Application.Users.Dtos;

namespace UserManagement.Application.Users.Validators
{
    public class AuthRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public AuthRequestValidator()
        {
            RuleFor(a => a.UserName).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(a => a.Password).NotEmpty().MinimumLength(6).MaximumLength(20);
        }
    }
}
