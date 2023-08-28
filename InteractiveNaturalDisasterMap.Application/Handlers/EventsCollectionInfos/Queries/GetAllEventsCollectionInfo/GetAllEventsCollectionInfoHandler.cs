using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Queries.GetAllEventsCollectionInfo
{
    public class GetAllEventsCollectionInfoHandler : IRequestHandler<GetAllEventsCollectionInfoRequest, IList<EventsCollectionInfoDto>>
    {
        private readonly IGenericBaseEntityRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;

        public GetAllEventsCollectionInfoHandler(IUnitOfWork unitOfWork)
        {
            _eventsCollectionInfoRepository = unitOfWork.EventsCollectionInfoRepository;
        }

        public async Task<IList<EventsCollectionInfoDto>> Handle(GetAllEventsCollectionInfoRequest request, CancellationToken cancellationToken)
        {
            var eventsCollectionInfos = await _eventsCollectionInfoRepository.GetAllAsync(cancellationToken, eci => eci.User);
            IList<EventsCollectionInfoDto> eventsCollectionInfoDtos = new List<EventsCollectionInfoDto>(); 
            foreach (var eventsCollectionInfo in eventsCollectionInfos)
            {
                eventsCollectionInfoDtos.Add(new EventsCollectionInfoDto(eventsCollectionInfo));
            }

            return eventsCollectionInfoDtos;
        }
    }
}
