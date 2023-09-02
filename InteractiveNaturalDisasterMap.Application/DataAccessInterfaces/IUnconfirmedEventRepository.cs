using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IUnconfirmedEventRepository : IBaseRepository<UnconfirmedEvent>
    {
        Task<UnconfirmedEvent?> GetByEventId(int eventId,
            CancellationToken cancellationToken,
            params Expression<Func<UnconfirmedEvent, object>>[] includes);
    }
}
