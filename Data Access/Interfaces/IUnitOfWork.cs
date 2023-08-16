namespace Data_Access.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new();

        Task SaveAsync();
    }
}
