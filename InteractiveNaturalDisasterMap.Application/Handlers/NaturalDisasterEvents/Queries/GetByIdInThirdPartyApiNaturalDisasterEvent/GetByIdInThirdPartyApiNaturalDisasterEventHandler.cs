using System.Linq.Expressions;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using MediatR;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Queries.GetByIdInThirdPartyApiNaturalDisasterEvent
{
    public class GetByIdInThirdPartyApiNaturalDisasterEventHandler : IRequestHandler<GetByIdInThirdPartyApiNaturalDisasterEventRequest, NaturalDisasterEventDto>
    {
        private readonly INaturalDisasterEventRepository _naturalDisasterEventRepository;

        public GetByIdInThirdPartyApiNaturalDisasterEventHandler(IUnitOfWork unitOfWork)
        {
            _naturalDisasterEventRepository = unitOfWork.NaturalDisasterEventRepository;
        }

        public async Task<NaturalDisasterEventDto> Handle(GetByIdInThirdPartyApiNaturalDisasterEventRequest request, CancellationToken cancellationToken)
        {
            Expression<Func<NaturalDisasterEvent, bool>> filter = nte => nte.IdInThirdPartyApi == request.GetByIdNaturalDisasterEventDto.Id;
            var naturalDisasterEvent = (await _naturalDisasterEventRepository.GetAllAsync(cancellationToken, filter))
                .FirstOrDefault() ?? throw new NotFoundException(nameof(NaturalDisasterEvent), request.GetByIdNaturalDisasterEventDto.Id);

            return new NaturalDisasterEventDto(naturalDisasterEvent);
        }
    }
}
