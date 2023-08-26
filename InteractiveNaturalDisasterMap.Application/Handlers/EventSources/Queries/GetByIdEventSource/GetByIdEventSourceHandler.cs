using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Queries.GetByIdEventSource
{
    public class GetByIdEventSourceHandler : IRequestHandler<GetByIdEventSourceRequest, EventSourceDto>
    {
        private readonly IGenericBaseEntityRepository<EventSource> _eventSourceRepository;

        public GetByIdEventSourceHandler(IUnitOfWork unitOfWork)
        {
            _eventSourceRepository = unitOfWork.EventSourceRepository;
        }

        public async Task<EventSourceDto> Handle(GetByIdEventSourceRequest request, CancellationToken cancellationToken)
        {
            var eventSource = await _eventSourceRepository.GetByIdAsync(request.GetByIdEventSourceDto.Id, cancellationToken) 
                                ?? throw new NotFoundException(nameof(EventSource), request.GetByIdEventSourceDto.Id);

            return new EventSourceDto(eventSource);
        }
    }
}
