using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.UpdateEventSource
{
    public class UpdateEventSourceHandler : IRequestHandler<UpdateEventSourceRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventSource> _eventSourceRepository;

        public UpdateEventSourceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventSourceRepository = unitOfWork.EventSourceRepository;
        }

        public async Task Handle(UpdateEventSourceRequest request, CancellationToken cancellationToken)
        {
            var eventSource = await _eventSourceRepository.GetByIdAsync(request.UpdateEventSourceDto.Id, cancellationToken) 
                              ?? throw new NotFoundException(nameof(EventSource), request.UpdateEventSourceDto.Id);

            eventSource.SourceType = request.UpdateEventSourceDto.SourceType;
            _eventSourceRepository.Update(eventSource);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
