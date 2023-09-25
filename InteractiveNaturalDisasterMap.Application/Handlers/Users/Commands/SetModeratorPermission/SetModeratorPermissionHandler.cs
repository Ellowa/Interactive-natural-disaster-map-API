using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.Users.Commands.SetModeratorPermission
{
    public class SetModeratorPermissionHandler : IRequestHandler<SetModeratorPermissionRequest>
    {
        private readonly IGenericBaseEntityRepository<User> _userRepository;
        private readonly IGenericBaseEntityRepository<UserRole> _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetModeratorPermissionHandler(IUnitOfWork unitOfWork, IGenericBaseEntityRepository<UserRole> userRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _userRoleRepository = userRoleRepository;
            _userRepository = unitOfWork.UserRepository;
        }

        public async Task Handle(SetModeratorPermissionRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.SetModeratorPermissionDto.Id, cancellationToken, u => u.Role)
                       ?? throw new NotFoundException(nameof(User), request.SetModeratorPermissionDto.Id);
            var moderatorRole = (await _userRoleRepository.GetAllAsync(cancellationToken, role => role.RoleName == "moderator"))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(UserRole), "moderator");

            user.RoleId = moderatorRole.Id;
            _userRepository.Update(user);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
