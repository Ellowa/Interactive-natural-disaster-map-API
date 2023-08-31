using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IEventsCollectionRepository : IBaseRepository<EventsCollection>
    {
        Task<EventsCollection?> GetByCollectionName(string collectionName,
            CancellationToken cancellationToken,
            params Expression<Func<EventsCollection, object>>[] includes);
    }
}
