using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IEventsCollectionRepository : IBaseRepository<EventsCollection>
    {
        Task<IReadOnlyList<EventsCollection>> GetByEventIdAsync(int eventId,
            CancellationToken cancellationToken,
            params Expression<Func<EventsCollection, object>>[] includes);

        Task<IReadOnlyList<EventsCollection>> GetByUserIdAsync(int userId,
            CancellationToken cancellationToken,
            params Expression<Func<EventsCollection, object>>[] includes);

        Task<EventsCollection?> GetByCollectionName(string collectionName,
            CancellationToken cancellationToken,
            params Expression<Func<EventsCollection, object>>[] includes);
    }
}
