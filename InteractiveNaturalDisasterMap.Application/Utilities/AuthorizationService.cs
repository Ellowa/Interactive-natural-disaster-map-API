using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Interfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Utilities
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AuthorizeAsync(int currentUserId, int resourceUserId, CancellationToken cancellationToken, object? resource)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(currentUserId, cancellationToken, u => u.Role) ??
                throw new NotFoundException(nameof(User), currentUserId);

            if (user.Role.RoleName == "moderator") return true;

            if (currentUserId != resourceUserId)
                throw new AuthorizationException(nameof(resource), currentUserId);

            return true;
        }
    }
}
