using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Queries.GetAllEventsCollectionInfo
{
    public class GetAllEventsCollectionInfoByUserIdHandler : IRequestHandler<GetAllEventsCollectionInfoByUserIdRequest, IList<EventsCollectionInfoDto>>
    {
        private readonly IGenericBaseEntityRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;

        public GetAllEventsCollectionInfoByUserIdHandler(IUnitOfWork unitOfWork)
        {
            _eventsCollectionInfoRepository = unitOfWork.EventsCollectionInfoRepository;
        }

        public async Task<IList<EventsCollectionInfoDto>> Handle(GetAllEventsCollectionInfoByUserIdRequest request, CancellationToken cancellationToken)
        {
            var eventsCollectionInfos = (await _eventsCollectionInfoRepository.GetAllAsync(cancellationToken, eci => eci.User))
                .Where(eci => eci.UserId == request.GetAllEventsCollectionInfoDto.UserId);
            IList<EventsCollectionInfoDto> eventsCollectionInfoDtos = new List<EventsCollectionInfoDto>(); 
            foreach (var eventsCollectionInfo in eventsCollectionInfos)
            {
                eventsCollectionInfoDtos.Add(new EventsCollectionInfoDto(eventsCollectionInfo));
            }

            return eventsCollectionInfoDtos;
        }
    }
}
