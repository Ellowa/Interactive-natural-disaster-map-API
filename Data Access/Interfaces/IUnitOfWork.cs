namespace Data_Access.Interfaces
{
    internal interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new();

        Task SaveAsync();
    }
}
