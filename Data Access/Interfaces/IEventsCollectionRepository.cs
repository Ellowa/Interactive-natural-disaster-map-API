using System.Linq.Expressions;
using Data_Access.Entities;

namespace Data_Access.Interfaces
{
    public interface IEventsCollectionRepository : IBaseRepository<EventsCollection>
    {
        Task<EventsCollection?> GetByEventIdAsync(int eventId,
            params Expression<Func<EventsCollection, object>>[] includes);

        Task<EventsCollection?> GetByUserIdAsync(int userId,
            params Expression<Func<EventsCollection, object>>[] includes);

        Task<EventsCollection?> GetByCollectionName(string collectionName,
            params Expression<Func<EventsCollection, object>>[] includes);
    }
}
