﻿using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Infrastructure.Authorization
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AuthorizeAsync(int currentUserId, int resourceUserId, CancellationToken cancellationToken, object? resource, int? resourceId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(currentUserId, cancellationToken, u => u.Role) ??
                throw new NotFoundException(nameof(User), currentUserId);

            if (user.Role.RoleName == "moderator") return true;

            if (currentUserId != resourceUserId)
                throw new AuthorizationException($"{resource?.GetType()}(id - {resourceId})", currentUserId);

            return true;
        }
    }
}
