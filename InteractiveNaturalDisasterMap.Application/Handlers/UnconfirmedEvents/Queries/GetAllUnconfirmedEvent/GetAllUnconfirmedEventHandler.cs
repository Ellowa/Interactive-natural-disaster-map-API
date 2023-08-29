using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.Queries.GetAllUnconfirmedEvent
{
    public class GetAllUnconfirmedEventHandler : IRequestHandler<GetAllUnconfirmedEventRequest, IList<UnconfirmedEventDto>>
    {
        private readonly IUnconfirmedEventRepository _unconfirmedEventRepository;

        public GetAllUnconfirmedEventHandler(IUnitOfWork unitOfWork)
        {
            _unconfirmedEventRepository = unitOfWork.UnconfirmedEventRepository;
        }

        public async Task<IList<UnconfirmedEventDto>> Handle(GetAllUnconfirmedEventRequest request, CancellationToken cancellationToken)
        {
            var unconfirmedEvents = await _unconfirmedEventRepository.GetAllAsync(cancellationToken, ue => ue.User, ue => ue.Event);
            IList<UnconfirmedEventDto> unconfirmedEventDtos = new List<UnconfirmedEventDto>(); 
            foreach (var unconfirmedEvent in unconfirmedEvents)
            {
                unconfirmedEventDtos.Add(new UnconfirmedEventDto(unconfirmedEvent));
            }

            return unconfirmedEventDtos;
        }
    }
}
