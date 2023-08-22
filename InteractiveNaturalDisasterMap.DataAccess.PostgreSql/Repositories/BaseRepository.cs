using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
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

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity != null)
                DbSet.Remove(entity);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
