using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.CreateEventsCollectionInfo
{
    public class CreateEventsCollectionInfoHandler : IRequestHandler<CreateEventsCollectionInfoRequest, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;

        public CreateEventsCollectionInfoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventsCollectionInfoRepository = unitOfWork.EventsCollectionInfoRepository;
        }

        public async Task<int> Handle(CreateEventsCollectionInfoRequest request, CancellationToken cancellationToken)
        {
            var entity = request.CreateEventsCollectionInfoDto.Map(request.UserId);
            await _eventsCollectionInfoRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
            return entity.Id;
        }
    }
}
