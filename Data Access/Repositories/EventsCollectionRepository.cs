using System.Linq.Expressions;
using Data_Access.Entities;
using Data_Access.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data_Access.Repositories
{
    public class EventsCollectionRepository : BaseRepository<EventsCollection>, IEventsCollectionRepository
    {
        public EventsCollectionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<EventsCollection?> GetByEventIdAsync(int eventId,
            params Expression<Func<EventsCollection, object>>[] includes)
        {
            IQueryable<EventsCollection> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.EventId == eventId);
        }

        public async Task<EventsCollection?> GetByUserIdAsync(int userId,
            params Expression<Func<EventsCollection, object>>[] includes)
        {
            IQueryable<EventsCollection> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.UserId == userId);
        }

        public async Task<EventsCollection?> GetByCollectionName(string collectionName,
            params Expression<Func<EventsCollection, object>>[] includes)
        {
            IQueryable<EventsCollection> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.EventsCollectionInfo.CollectionName == collectionName);
        }
    }
}
