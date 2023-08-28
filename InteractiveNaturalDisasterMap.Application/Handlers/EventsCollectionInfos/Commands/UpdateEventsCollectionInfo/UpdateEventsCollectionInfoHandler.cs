using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.UpdateEventsCollectionInfo
{
    public class UpdateEventsCollectionInfoHandler : IRequestHandler<UpdateEventsCollectionInfoRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;

        public UpdateEventsCollectionInfoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventsCollectionInfoRepository = unitOfWork.EventsCollectionInfoRepository;
        }

        public async Task Handle(UpdateEventsCollectionInfoRequest request, CancellationToken cancellationToken)
        {
            if (await _eventsCollectionInfoRepository.GetByIdAsync(request.UpdateEventsCollectionInfoDto.Id, cancellationToken) == null)
                throw new NotFoundException(nameof(EventsCollectionInfo), request.UpdateEventsCollectionInfoDto.Id);

            _eventsCollectionInfoRepository.Update(request.UpdateEventsCollectionInfoDto.Map());
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
