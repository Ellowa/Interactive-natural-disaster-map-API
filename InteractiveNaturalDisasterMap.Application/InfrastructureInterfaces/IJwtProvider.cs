using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces
{
    public interface IJwtProvider
    {
        string Generate(User user);
    }
}