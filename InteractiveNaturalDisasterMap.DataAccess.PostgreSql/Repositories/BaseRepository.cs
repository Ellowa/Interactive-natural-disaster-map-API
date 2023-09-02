using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly InteractiveNaturalDisasterMapDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(InteractiveNaturalDisasterMapDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity, cancellationToken);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? filter, Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync(cancellationToken);
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
