using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories
{
    internal class EventsCollectionInfoRepository : GenericBaseEntityRepository<EventsCollectionInfo>, IEventsCollectionInfoRepository
    {
        public EventsCollectionInfoRepository(InteractiveNaturalDisasterMapDbContext context) : base(context)
        {
        }

        public override async Task<IReadOnlyList<EventsCollectionInfo>> GetAllAsync(CancellationToken cancellationToken,
            Expression<Func<EventsCollectionInfo, bool>>? filter,
            params Expression<Func<EventsCollectionInfo, object>>[] includes)
        {
            IQueryable<EventsCollectionInfo> query = DbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = query.Include(eci => eci.User)
                .Include(eci => eci.EventsCollection).ThenInclude(ec => ec.Event.Category)
                .Include(eci => eci.EventsCollection).ThenInclude(ec => ec.Event.Source)
                .Include(eci => eci.EventsCollection).ThenInclude(ec => ec.Event.MagnitudeUnit)
                .Include(eci => eci.EventsCollection).ThenInclude(ec => ec.Event.EventHazardUnit);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync(cancellationToken);
        }
    }
}
