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
            var collectionInfo = await _eventsCollectionInfoRepository.GetByIdAsync(request.DeleteEventsCollectionInfoDto.Id, cancellationToken)
                                 ?? throw new NotFoundException(nameof(EventsCollectionInfo), request.DeleteEventsCollectionInfoDto.Id);
            if (collectionInfo.User.Id != request.UserId)
                throw new AuthorizationException(nameof(collectionInfo), request.UserId);

            await _eventsCollectionInfoRepository.DeleteByIdAsync(request.DeleteEventsCollectionInfoDto.Id, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
