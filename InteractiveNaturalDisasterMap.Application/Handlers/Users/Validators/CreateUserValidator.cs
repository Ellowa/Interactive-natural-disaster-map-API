using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.CreateUser;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Validators
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(c => c.CreateUserDto.Login).Length(5, 20);
            RuleFor(c => c.CreateUserDto.Password)
                .NotEmpty().WithMessage("Password cannot be empty")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit")
                .Matches("[!@#$%^&*_]").WithMessage("Password must contain at least one special character (!@#$%^&*_)");
            RuleFor(c => c.CreateUserDto.Email)
                .EmailAddress()
                .When(c => c.CreateUserDto.Email != null);
        }
    }
}
