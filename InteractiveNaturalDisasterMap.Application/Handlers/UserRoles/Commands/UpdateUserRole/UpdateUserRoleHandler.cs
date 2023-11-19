using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.UpdateUserRole
{
    public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRoleRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<UserRole> _userRoleRepository;

        public UpdateUserRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRoleRepository = unitOfWork.UserRoleRepository;
        }

        public async Task Handle(UpdateUserRoleRequest request, CancellationToken cancellationToken)
        {
            var userRole = await _userRoleRepository.GetByIdAsync(request.UpdateUserRoleDto.Id, cancellationToken) 
                           ?? throw new NotFoundException(nameof(UserRole), request.UpdateUserRoleDto.Id);

            userRole.RoleName = request.UpdateUserRoleDto.RoleName;

            _userRoleRepository.Update(userRole);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
