using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventsCollectionInfos.Commands.DeleteEventsCollectionInfo
{
    public class DeleteEventsCollectionInfoHandler : IRequestHandler<DeleteEventsCollectionInfoRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericBaseEntityRepository<EventsCollectionInfo> _eventsCollectionInfoRepository;

        public DeleteEventsCollectionInfoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _eventsCollectionInfoRepository = unitOfWork.EventsCollectionInfoRepository;
        }

        public async Task Handle(DeleteEventsCollectionInfoRequest request, CancellationToken cancellationToken)
        {
            if(await _eventsCollectionInfoRepository.GetByIdAsync(request.DeleteEventsCollectionInfoDto.Id, cancellationToken) == null)
                throw new NotFoundException(nameof(EventsCollectionInfo), request.DeleteEventsCollectionInfoDto.Id);

            await _eventsCollectionInfoRepository.DeleteByIdAsync(request.DeleteEventsCollectionInfoDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
