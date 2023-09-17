using InteractiveNaturalDisasterMap.Domain.Entities;
using System.Linq.Expressions;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IEventsCollectionInfoRepository : IGenericBaseEntityRepository<EventsCollectionInfo>
    {
        new Task<IReadOnlyList<EventsCollectionInfo>> GetAllAsync(
            CancellationToken cancellationToken,
            Expression<Func<EventsCollectionInfo, bool>>? filter,
            params Expression<Func<EventsCollectionInfo, object>>[] includes);
    }
}
