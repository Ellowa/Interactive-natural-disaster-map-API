namespace InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces
{
    public interface IAuthorizationService
    {
        Task<bool> AuthorizeAsync(int currentUserId, int resourceUserId, CancellationToken cancellationToken, object? resource);
    }
}