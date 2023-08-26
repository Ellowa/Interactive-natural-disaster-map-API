using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories
{
    public class GenericBaseEntityRepository<TEntity> : BaseRepository<TEntity>, IGenericBaseEntityRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        public GenericBaseEntityRepository(InteractiveNaturalDisasterMapDbContext context) : base(context)
        {
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await DbSet.FindAsync(id, cancellationToken);
            if (entity != null)
                DbSet.Remove(entity);
        }
    }
}
