using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.DTOs;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Validators
{
    public sealed class LoginUserValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserValidator()
        {
            RuleFor(l => l.Login).NotEmpty();
            RuleFor(l => l.Password).NotEmpty();
        }
    }
}
