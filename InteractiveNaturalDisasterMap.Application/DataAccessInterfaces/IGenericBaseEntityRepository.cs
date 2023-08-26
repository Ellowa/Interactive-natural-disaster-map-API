using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IGenericBaseEntityRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes);

        Task DeleteByIdAsync(int id, CancellationToken cancellationToken);
    }
}
