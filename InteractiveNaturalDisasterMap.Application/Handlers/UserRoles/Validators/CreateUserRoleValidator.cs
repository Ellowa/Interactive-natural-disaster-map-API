using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.CreateUserRole;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Validators
{
    public sealed class CreateEventHazardUnitsValidator : AbstractValidator<CreateUserRoleRequest>
    {
        public CreateEventHazardUnitsValidator()
        {
            RuleFor(c => c.CreateUserRoleDto.RoleName).NotEmpty();
        }
    }
}
