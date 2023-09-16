using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.LoginUser;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Validators
{
    public sealed class LoginUserValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginUserValidator()
        {
            RuleFor(l => l.LoginUserDto.Login).NotEmpty();
            RuleFor(l => l.LoginUserDto.Password).NotEmpty();
        }
    }
}
