using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using MediatR;
using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetAllNaturalDisasterEvent
{
    public class GetAllNaturalDisasterEventHandler : IRequestHandler<GetAllNaturalDisasterEventRequest, IList<NaturalDisasterEventDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INaturalDisasterEventRepository _naturalDisasterEventRepository;

        public GetAllNaturalDisasterEventHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _naturalDisasterEventRepository = unitOfWork.NaturalDisasterEventRepository;
        }

        public async Task<IList<NaturalDisasterEventDto>> Handle(GetAllNaturalDisasterEventRequest request, CancellationToken cancellationToken)
        {
            if (request.GetAllNaturalDisasterEventDto.ExtendedPeriodEndPoint != null &&
                (request.GetAllNaturalDisasterEventDto.ExtendedPeriodEndPoint < (DateTime.Now - TimeSpan.FromDays(1827)).ToUniversalTime() ||
                 request.GetAllNaturalDisasterEventDto.ExtendedPeriodEndPoint > DateTime.Now.ToUniversalTime()))
                throw new RequestArgumentException(nameof(request.GetAllNaturalDisasterEventDto.ExtendedPeriodEndPoint), request.GetAllNaturalDisasterEventDto.ExtendedPeriodEndPoint);

            Expression<Func<NaturalDisasterEvent, bool>> filter = request.GetAllNaturalDisasterEventDto.ExtendedPeriodEndPoint == null
                ? nde => nde.Confirmed && nde.StartDate >= (DateTime.Now - TimeSpan.FromDays(366)).ToUniversalTime()
                : nde => nde.Confirmed && nde.StartDate >= request.GetAllNaturalDisasterEventDto.ExtendedPeriodEndPoint.Value.ToUniversalTime();
            IEnumerable<NaturalDisasterEvent> naturalDisasterEvents = await _naturalDisasterEventRepository.GetAllAsync(cancellationToken, filter,
                nde => nde.Category, nde => nde.Source, nde => nde.MagnitudeUnit, nde => nde.EventHazardUnit);
            
            IEnumerable<NaturalDisasterEvent> userUnconfirmedEvents = new List<NaturalDisasterEvent>();
            if (request.UserId != null)
            {
                userUnconfirmedEvents = (await _unitOfWork.UnconfirmedEventRepository
                    .GetAllAsync(cancellationToken, ue=> ue.UserId == request.UserId, 
                        ue => ue.Event.Category,
                        ue => ue.Event.MagnitudeUnit,
                        ue => ue.Event.EventHazardUnit,
                        ue => ue.Event.Source))
                    .Select(ue => ue.Event);
            }
            naturalDisasterEvents = naturalDisasterEvents.Union(userUnconfirmedEvents);

            IList<NaturalDisasterEventDto> naturalDisasterEventDtos = new List<NaturalDisasterEventDto>(); 
            foreach (var naturalDisasterEvent in naturalDisasterEvents)
            {
                naturalDisasterEventDtos.Add(new NaturalDisasterEventDto(naturalDisasterEvent));
            }

            return naturalDisasterEventDtos;
        }
    }
}
