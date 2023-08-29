using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Queries.GetAllUserRole
{
    public class GetAllUserRoleHandler : IRequestHandler<GetAllUserRoleRequest, IList<UserRoleDto>>
    {
        private readonly IGenericBaseEntityRepository<UserRole> _userRoleRepository;

        public GetAllUserRoleHandler(IUnitOfWork unitOfWork)
        {
            _userRoleRepository = unitOfWork.UserRoleRepository;
        }

        public async Task<IList<UserRoleDto>> Handle(GetAllUserRoleRequest request, CancellationToken cancellationToken)
        {
            var userRoles = await _userRoleRepository.GetAllAsync(cancellationToken);
            IList<UserRoleDto> userRoleDtos = new List<UserRoleDto>(); 
            foreach (var userRole in userRoles)
            {
                userRoleDtos.Add(new UserRoleDto(userRole));
            }

            return userRoleDtos;
        }
    }
}
