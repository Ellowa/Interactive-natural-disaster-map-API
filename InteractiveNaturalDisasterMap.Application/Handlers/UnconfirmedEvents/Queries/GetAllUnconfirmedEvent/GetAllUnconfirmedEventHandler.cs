using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.UnconfirmedEvents.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
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
            Expression<Func<UnconfirmedEvent, bool>>? filter =
                request.GetAllUnconfirmedEventDto.AddIsChecked == null ||
                request.GetAllUnconfirmedEventDto.AddIsChecked == false
                    ? ue => !ue.IsChecked
                    : null;
            var unconfirmedEvents = await _unconfirmedEventRepository.GetAllAsync(cancellationToken, filter,
                ue => ue.User.Role, 
                ue => ue.Event.Category,
                ue => ue.Event.Source,
                ue => ue.Event.MagnitudeUnit,
                ue => ue.Event.EventHazardUnit);
            IList<UnconfirmedEventDto> unconfirmedEventDtos = new List<UnconfirmedEventDto>(); 
            foreach (var unconfirmedEvent in unconfirmedEvents)
            {
                unconfirmedEventDtos.Add(new UnconfirmedEventDto(unconfirmedEvent));
            }

            return unconfirmedEventDtos;
        }
    }
}
