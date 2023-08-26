using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IUnconfirmedEventRepository : IBaseRepository<UnconfirmedEvent>
    {
        Task<IReadOnlyList<UnconfirmedEvent>> GetByUserId(int userId,
            CancellationToken cancellationToken,
            params Expression<Func<UnconfirmedEvent, object>>[] includes);

        Task<UnconfirmedEvent?> GetByEventId(int eventId,
            CancellationToken cancellationToken,
            params Expression<Func<UnconfirmedEvent, object>>[] includes);
    }
}
