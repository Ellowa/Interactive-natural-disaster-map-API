using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericBaseEntityRepository<EventCategory> EventCategoryRepository { get; }
        INaturalDisasterEventRepository NaturalDisasterEventRepository { get; }
        IGenericBaseEntityRepository<EventCoordinate> EventCoordinateRepository { get; }
        IGenericBaseEntityRepository<EventSource> EventSourceRepository { get; }
        IGenericBaseEntityRepository<MagnitudeUnit> MagnitudeUnitRepository { get; }
        IEventsCollectionRepository EventsCollectionRepository { get; }
        IGenericBaseEntityRepository<EventsCollectionInfo> EventsCollectionInfoRepository { get; }
        IGenericBaseEntityRepository<User> UserRepository { get; }
        IGenericBaseEntityRepository<UserRole> UserRoleRepository { get; }
        IUnconfirmedEventRepository UnconfirmedEventRepository { get; }
        IGenericBaseEntityRepository<EventHazardUnit> EventHazardUnitRepository { get; }

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
