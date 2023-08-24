using System.Linq.Expressions;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        void Update(TEntity entity);
    }
}
