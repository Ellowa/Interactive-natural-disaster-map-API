using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.EventSources.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventSources.Queries.GetAllEventSource
{
    public class GetAllEventSourceHandler : IRequestHandler<GetAllEventSourceRequest, IList<EventSourceDto>>
    {
        private readonly IGenericBaseEntityRepository<EventSource> _eventSourceRepository;

        public GetAllEventSourceHandler(IUnitOfWork unitOfWork)
        {
            _eventSourceRepository = unitOfWork.EventSourceRepository;
        }

        public async Task<IList<EventSourceDto>> Handle(GetAllEventSourceRequest request, CancellationToken cancellationToken)
        {
            var eventSources = (await _eventSourceRepository.GetAllAsync(cancellationToken, null)).OrderBy(es => es.Id);
            IList<EventSourceDto> eventSourceDtos = new List<EventSourceDto>(); 
            foreach (var eventSource in eventSources)
            {
                eventSourceDtos.Add(new EventSourceDto(eventSource));
            }

            return eventSourceDtos;
        }
    }
}
