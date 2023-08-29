using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Commands.CreateUnconfirmedEvent
{
    public class CreateUnconfirmedEventHandler : IRequestHandler<CreateUnconfirmedEventRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnconfirmedEventRepository _unconfirmedEventRepository;

        public CreateUnconfirmedEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _unconfirmedEventRepository = unitOfWork.UnconfirmedEventRepository;
        }

        public async Task Handle(CreateUnconfirmedEventRequest request, CancellationToken cancellationToken)
        {
            var entity = request.CreateUnconfirmedEventDto.Map();
            await _unconfirmedEventRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
