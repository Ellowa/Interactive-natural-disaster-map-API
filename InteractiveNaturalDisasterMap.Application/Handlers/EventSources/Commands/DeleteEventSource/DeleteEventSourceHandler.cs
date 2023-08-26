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
            if(await _eventSourceRepository.GetByIdAsync(request.DeleteEventSourceDto.Id, cancellationToken) == null)
                throw new NotFoundException(nameof(EventSource), request.DeleteEventSourceDto.Id);

            await _eventSourceRepository.DeleteByIdAsync(request.DeleteEventSourceDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
