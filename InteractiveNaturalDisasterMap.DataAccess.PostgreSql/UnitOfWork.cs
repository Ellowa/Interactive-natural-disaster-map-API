using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InteractiveNaturalDisasterMapDbContext _context;

        private IGenericBaseEntityRepository<EventCategory> _eventCategoryRepository;
        private IGenericBaseEntityRepository<EventCoordinate> _eventCoordinateRepository;
        private IGenericBaseEntityRepository<EventSource> _eventSourceRepository;
        private IGenericBaseEntityRepository<MagnitudeUnit> _magnitudeUnitRepository;
        private IEventsCollectionRepository _eventsCollectionRepository;
        private IGenericBaseEntityRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;
        private IGenericBaseEntityRepository<User> _userRepository;
        private IGenericBaseEntityRepository<UserRole> _userRoleRepository;
        private IUnconfirmedEventRepository _unconfirmedEventRepository;
        private INaturalDisasterEventRepository _naturalDisasterEventRepository;


        public UnitOfWork(InteractiveNaturalDisasterMapDbContext context)
        {
            this._context = context;
        }

        public IGenericBaseEntityRepository<EventCategory> EventCategoryRepository =>
            _eventCategoryRepository ??= new GenericBaseEntityRepository<EventCategory>(_context);

        public IGenericBaseEntityRepository<NaturalDisasterEvent> NaturalDisasterEventRepository =>
            _naturalDisasterEventRepository ??= new NaturalDisasterEventRepository(_context);

        public IGenericBaseEntityRepository<EventCoordinate> EventCoordinateRepository =>
            _eventCoordinateRepository ??= new GenericBaseEntityRepository<EventCoordinate>(_context);

        public IGenericBaseEntityRepository<EventSource> EventSourceRepository =>
            _eventSourceRepository ??= new GenericBaseEntityRepository<EventSource>(_context);

        public IGenericBaseEntityRepository<MagnitudeUnit> MagnitudeUnitRepository =>
            _magnitudeUnitRepository ??= new GenericBaseEntityRepository<MagnitudeUnit>(_context);

        public IEventsCollectionRepository EventsCollectionRepository =>
            _eventsCollectionRepository ??= new EventsCollectionRepository(_context);

        public IGenericBaseEntityRepository<EventsCollectionInfo> EventsCollectionInfoRepository =>
            _eventsCollectionInfoRepository ??= new GenericBaseEntityRepository<EventsCollectionInfo>(_context);

        public IGenericBaseEntityRepository<User> UserRepository =>
            _userRepository ??= new GenericBaseEntityRepository<User>(_context);

        public IGenericBaseEntityRepository<UserRole> UserRoleRepository =>
            _userRoleRepository ??= new GenericBaseEntityRepository<UserRole>(_context);

        public IUnconfirmedEventRepository UnconfirmedEventRepository =>
            _unconfirmedEventRepository ??= new UnconfirmedEventRepository(_context);


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task SaveAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
