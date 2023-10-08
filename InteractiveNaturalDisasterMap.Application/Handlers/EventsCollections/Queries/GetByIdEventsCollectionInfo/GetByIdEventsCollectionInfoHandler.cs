using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollections.Queries.GetByIdEventsCollectionInfo
{
    public class GetByIdEventsCollectionInfoHandler : IRequestHandler<GetByIdEventsCollectionInfoRequest, EventsCollectionInfoDto>
    {
        private readonly IGenericBaseEntityRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;

        public GetByIdEventsCollectionInfoHandler(IUnitOfWork unitOfWork)
        {
            _eventsCollectionInfoRepository = unitOfWork.EventsCollectionInfoRepository;
        }

        public async Task<EventsCollectionInfoDto> Handle(GetByIdEventsCollectionInfoRequest request, CancellationToken cancellationToken)
        {
            var eventsCollectionInfo = await _eventsCollectionInfoRepository.GetByIdAsync(request.GetByIdEventsCollectionInfoDto.Id, cancellationToken, eci => eci.User) 
                                ?? throw new NotFoundException(nameof(EventsCollectionInfo), request.GetByIdEventsCollectionInfoDto.Id);

            return new EventsCollectionInfoDto(eventsCollectionInfo);
        }
    }
}
