using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Entities;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IUnconfirmedEventRepository : IBaseRepository<UnconfirmedEvent>
    {
        Task<IReadOnlyList<UnconfirmedEvent>> GetByUserId(int userId,
            params Expression<Func<UnconfirmedEvent, object>>[] includes);

        Task<UnconfirmedEvent?> GetByEventId(int eventId,
            params Expression<Func<UnconfirmedEvent, object>>[] includes);
    }
}
