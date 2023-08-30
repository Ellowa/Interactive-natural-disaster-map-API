using System.Linq.Expressions;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? filter, params Expression<Func<TEntity, object>>[] includes);

        Task AddAsync(TEntity entity, CancellationToken cancellationToken);

        void Delete(TEntity entity);

        void Update(TEntity entity);
    }
}
