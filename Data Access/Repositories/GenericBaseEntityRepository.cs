using System.Linq.Expressions;
using Data_Access.Entities;
using Data_Access.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data_Access.Repositories
{
    public class GenericBaseEntityRepository<TEntity> : BaseRepository<TEntity>, IGenericBaseEntityRepository<TEntity>
        where TEntity : BaseEntity, new()
    {
        public GenericBaseEntityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
