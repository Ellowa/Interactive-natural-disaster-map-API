using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Entities;

namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IGenericBaseEntityRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);
    }
}
