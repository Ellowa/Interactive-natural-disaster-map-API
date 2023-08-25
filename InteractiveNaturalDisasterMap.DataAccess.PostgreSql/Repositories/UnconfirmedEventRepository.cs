using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories
{
    public class UnconfirmedEventRepository : BaseRepository<UnconfirmedEvent>, IUnconfirmedEventRepository
    {
        public UnconfirmedEventRepository(InteractiveNaturalDisasterMapDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<UnconfirmedEvent>> GetByUserId(int userId, params Expression<Func<UnconfirmedEvent, object>>[] includes)
        {
            IQueryable<UnconfirmedEvent> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(e => e.UserId == userId).ToListAsync();
        }

        public async Task<UnconfirmedEvent?> GetByEventId(int eventId, params Expression<Func<UnconfirmedEvent, object>>[] includes)
        {
            IQueryable<UnconfirmedEvent> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.EventId == eventId);
        }
    }
}
