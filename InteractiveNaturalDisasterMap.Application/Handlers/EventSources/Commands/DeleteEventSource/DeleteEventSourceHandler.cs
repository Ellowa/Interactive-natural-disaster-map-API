using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Commands.DeleteEventSource
{
    public class DeleteEventSourceHandler : IRequestHandler<DeleteEventSourceRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventSource> _eventSourceRepository;

        public DeleteEventSourceHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventSourceRepository = unitOfWork.EventSourceRepository;
        }

        public async Task Handle(DeleteEventSourceRequest request, CancellationToken cancellationToken)
        {
            if (await _eventSourceRepository.GetByIdAsync(request.DeleteEventSourceDto.Id, cancellationToken) == null)
                throw new NotFoundException(nameof(EventSource), request.DeleteEventSourceDto.Id);

            var events = await _unitOfWork.NaturalDisasterEventRepository.GetAllAsync(
                cancellationToken,
                nde => nde.SourceId == request.DeleteEventSourceDto.Id);
            var unknownSourceType = (await _eventSourceRepository.GetAllAsync(
                               cancellationToken,
                               es => es.SourceType == "unknown")).FirstOrDefault()
                           ?? throw new NotFoundException(nameof(EventSource), "With name unknown");

            foreach (NaturalDisasterEvent naturalDisasterEvent in events)
            {
                naturalDisasterEvent.SourceId = unknownSourceType.Id;
                _unitOfWork.NaturalDisasterEventRepository.Update(naturalDisasterEvent);
            }

            await _eventSourceRepository.DeleteByIdAsync(request.DeleteEventSourceDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
