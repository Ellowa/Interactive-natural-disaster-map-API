namespace InteractiveNaturalDisasterMap.Application.DataAccessInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new();

        Task SaveAsync();
    }
}
