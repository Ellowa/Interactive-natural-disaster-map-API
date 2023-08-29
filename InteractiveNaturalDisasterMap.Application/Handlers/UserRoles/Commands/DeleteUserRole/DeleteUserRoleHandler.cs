using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.DeleteUserRole
{
    public class DeleteUserRoleHandler : IRequestHandler<DeleteUserRoleRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<UserRole> _userRoleRepository;

        public DeleteUserRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRoleRepository = unitOfWork.UserRoleRepository;
        }

        public async Task Handle(DeleteUserRoleRequest request, CancellationToken cancellationToken)
        {
            if(await _userRoleRepository.GetByIdAsync(request.DeleteUserRoleDto.Id, cancellationToken) == null)
                throw new NotFoundException(nameof(UserRole), request.DeleteUserRoleDto.Id);

            await _userRoleRepository.DeleteByIdAsync(request.DeleteUserRoleDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
