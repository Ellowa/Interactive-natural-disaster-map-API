using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Queries.GetByIdUserRole
{
    public class GetByIdUserRoleHandler : IRequestHandler<GetByIdUserRoleRequest, UserRoleDto>
    {
        private readonly IGenericBaseEntityRepository<UserRole> _userRoleRepository;

        public GetByIdUserRoleHandler(IUnitOfWork unitOfWork)
        {
            _userRoleRepository = unitOfWork.UserRoleRepository;
        }

        public async Task<UserRoleDto> Handle(GetByIdUserRoleRequest request,
            CancellationToken cancellationToken)
        {
            var userRole =
                await _userRoleRepository.GetByIdAsync(request.GetByIdUserRoleDto.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(UserRole), request.GetByIdUserRoleDto.Id);

            return new UserRoleDto(userRole);
        }
    }
}
