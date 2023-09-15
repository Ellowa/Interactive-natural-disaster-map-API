using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeAsync(int currentUserId, int resourceUserId, CancellationToken cancellationToken, BaseEntity? resource);
    }
}