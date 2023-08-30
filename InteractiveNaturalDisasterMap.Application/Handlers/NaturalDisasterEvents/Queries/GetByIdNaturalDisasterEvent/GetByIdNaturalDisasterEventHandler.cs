using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetByIdNaturalDisasterEvent
{
    public class GetByIdNaturalDisasterEventHandler : IRequestHandler<GetByIdNaturalDisasterEventRequest, NaturalDisasterEventDto>
    {
        private readonly INaturalDisasterEventRepository _naturalDisasterEventRepository;

        public GetByIdNaturalDisasterEventHandler(IUnitOfWork unitOfWork)
        {
            _naturalDisasterEventRepository = unitOfWork.NaturalDisasterEventRepository;
        }

        public async Task<NaturalDisasterEventDto> Handle(GetByIdNaturalDisasterEventRequest request, CancellationToken cancellationToken)
        {
            var naturalDisasterEvent = await _naturalDisasterEventRepository.GetByIdAsync(request.GetByIdNaturalDisasterEventDto.Id, cancellationToken, 
                                           nde => nde.Category, nde => nde.Source, nde => nde.MagnitudeUnit, nde => nde.EventHazardUnit) 
                                       ?? throw new NotFoundException(nameof(NaturalDisasterEvent), request.GetByIdNaturalDisasterEventDto.Id);

            return new NaturalDisasterEventDto(naturalDisasterEvent);
        }
    }
}
