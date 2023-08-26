using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.CreateEventSource
{
    public class CreateEventSourceHandler : IRequestHandler<CreateEventSourceRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventSource> _eventSourceRepository;

        public CreateEventSourceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventSourceRepository = unitOfWork.EventSourceRepository;
        }

        public async Task<int> Handle(CreateEventSourceRequest request, CancellationToken cancellationToken)
        {
            var entity = request.CreateEventSourceDto.Map();
            await _eventSourceRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return entity.Id;
        }
    }
}
