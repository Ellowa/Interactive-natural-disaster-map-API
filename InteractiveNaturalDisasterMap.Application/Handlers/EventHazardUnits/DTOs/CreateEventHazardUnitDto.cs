using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs
{
    public class CreateEventHazardUnitDto
    {
        public string HazardName { get; set; } = null!;

        public string MagnitudeUnitName { get; set; } = null!;

        public double? ThresholdValue { get; set; }

        public async Task<EventHazardUnit> MapAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            var magnitudeUnit = (await unitOfWork.MagnitudeUnitRepository.GetAllAsync(cancellationToken, mu => mu.MagnitudeUnitName == this.MagnitudeUnitName))
                                .FirstOrDefault() ?? throw new NotFoundException(nameof(MagnitudeUnit), $"With name {MagnitudeUnitName}");

            EventHazardUnit eventHazardUnit = new EventHazardUnit()
            {
                HazardName = this.HazardName,
                MagnitudeUnitId = magnitudeUnit.Id,
                ThresholdValue = this.ThresholdValue,
            };
            return eventHazardUnit;
        }
    }
}
