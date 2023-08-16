using System.Linq.Expressions;
using Data_Access.Entities;

namespace Data_Access.Interfaces
{
    public interface IGenericBaseEntityRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);
    }
}
