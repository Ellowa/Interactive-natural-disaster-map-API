namespace Data_Access.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new();

        Task SaveAsync();
    }
}
