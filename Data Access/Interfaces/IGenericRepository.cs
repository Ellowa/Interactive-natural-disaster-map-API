using System.Linq.Expressions;

namespace Data_Access.Interfaces
{
    internal interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetByIdAsync(int id, params Expression<Func<TEntity, object>>[] includes);

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteByIdAsync(int id);

        void Update(TEntity entity);
    }
}
