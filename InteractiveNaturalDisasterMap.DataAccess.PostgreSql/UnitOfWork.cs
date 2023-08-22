using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories;
using InteractiveNaturalDisasterMap.Entities;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InteractiveNaturalDisasterMapDbContext _context;

        private IBaseRepository<NaturalDisasterEvent> _naturalDisasterEventRepository;
        private IBaseRepository<EventCategory> _eventCategoryRepository;
        private IBaseRepository<EventCoordinate> _eventCoordinateRepository;
        private IBaseRepository<EventSource> _eventSourceRepository;
        private IBaseRepository<MagnitudeUnit> _magnitudeUnitRepository;
        private IBaseRepository<EventsCollection> _eventsCollectionRepository;
        private IBaseRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;
        private IBaseRepository<User> _useRepository;
        private IBaseRepository<UserRole> _userRoleRepository;

        private bool _disposed = false;

        public UnitOfWork(InteractiveNaturalDisasterMapDbContext context)
        {
            this._context = context;
            //Initialization of repositories
            _naturalDisasterEventRepository = new GenericBaseEntityRepository<NaturalDisasterEvent>(_context);
            _eventCategoryRepository = new GenericBaseEntityRepository<EventCategory>(_context);
            _eventCoordinateRepository = new GenericBaseEntityRepository<EventCoordinate>(_context);
            _eventSourceRepository = new GenericBaseEntityRepository<EventSource>(_context);
            _magnitudeUnitRepository = new GenericBaseEntityRepository<MagnitudeUnit>(_context);
            _eventsCollectionRepository = new EventsCollectionRepository(_context);
            _eventsCollectionInfoRepository = new GenericBaseEntityRepository<EventsCollectionInfo>(_context);
            _useRepository = new GenericBaseEntityRepository<User>(_context);
            _userRoleRepository = new GenericBaseEntityRepository<UserRole>(_context);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new()
        {
            TEntity entity = new TEntity();

            switch (entity)
            {
                case NaturalDisasterEvent _:
                    return (_naturalDisasterEventRepository as IBaseRepository<TEntity>)!;
                case EventCategory _:
                    return (_eventCategoryRepository as IBaseRepository<TEntity>)!;
                case EventCoordinate _:
                    return (_eventCoordinateRepository as IBaseRepository<TEntity>)!;
                case EventSource _:
                    return (_eventSourceRepository as IBaseRepository<TEntity>)!;
                case MagnitudeUnit _:
                    return (_magnitudeUnitRepository as IBaseRepository<TEntity>)!;
                case EventsCollectionRepository _:
                    return (_eventsCollectionRepository as IBaseRepository<TEntity>)!;
                case EventsCollectionInfo _:
                    return (_eventsCollectionInfoRepository as IBaseRepository<TEntity>)!;
                case User _:
                    return (_useRepository as IBaseRepository<TEntity>)!;
                case UserRole _:
                    return (_userRoleRepository as IBaseRepository<TEntity>)!;
                default:
                    throw new NotImplementedException();
            }
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
