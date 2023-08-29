using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UserRoles.Commands.CreateUserRole
{
    public class CreateUserRoleHandler : IRequestHandler<CreateUserRoleRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<UserRole> _userRoleRepository;

        public CreateUserRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRoleRepository = unitOfWork.UserRoleRepository;
        }

        public async Task<int> Handle(CreateUserRoleRequest request, CancellationToken cancellationToken)
        {
            var entity = request.CreateUserRoleDto.Map();
            await _userRoleRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return entity.Id;
        }
    }
}
