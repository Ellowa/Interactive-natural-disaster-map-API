using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.UpdateUserRole;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Validators
{
    public sealed class UpdateUserRoleValidator : AbstractValidator<UpdateUserRoleRequest>
    {
        public UpdateUserRoleValidator()
        {
            RuleFor(c => c.UpdateUserRoleDto.Id).NotNull();
            RuleFor(c => c.UpdateUserRoleDto.RoleName).NotEmpty();
        }
    }
}
