using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories
{
    public class NaturalDisasterEventRepository : GenericBaseEntityRepository<NaturalDisasterEvent>, INaturalDisasterEventRepository
    {
        public NaturalDisasterEventRepository(InteractiveNaturalDisasterMapDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<NaturalDisasterEvent>> GetAllByCategoryAsync(int categoryId, CancellationToken cancellationToken, params Expression<Func<NaturalDisasterEvent, object>>[] includes)
        {
            IQueryable<NaturalDisasterEvent> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(e => e.EventCategoryId == categoryId).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<NaturalDisasterEvent>> GetAllByMagnitudeUnitAsync(int magnitudeUnitId, CancellationToken cancellationToken, params Expression<Func<NaturalDisasterEvent, object>>[] includes)
        {
            IQueryable<NaturalDisasterEvent> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(e => e.MagnitudeUnitId == magnitudeUnitId).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<NaturalDisasterEvent>> GetAllByHazardUnitAsync(int hazardUnitId, CancellationToken cancellationToken, params Expression<Func<NaturalDisasterEvent, object>>[] includes)
        {
            IQueryable<NaturalDisasterEvent> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(e => e.EventHazardUnitId == hazardUnitId).ToListAsync(cancellationToken);
        }
    }
}
