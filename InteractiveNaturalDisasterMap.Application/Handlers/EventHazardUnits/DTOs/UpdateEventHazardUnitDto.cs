using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Exceptions;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs
{
    public class UpdateEventHazardUnitDto
    {
        public int Id { get; set; }

        public string HazardName { get; set; } = null!;

        public string MagnitudeUnitName { get; set; } = null!;

        public double? ThresholdValue { get; set; }

        public async Task<EventHazardUnit> MapAsync(IUnitOfWork unitOfWork, CancellationToken cancellationToken)
        {
            var magnitudeUnit = (await unitOfWork.MagnitudeUnitRepository.GetAllAsync(cancellationToken))
                                .FirstOrDefault(mu => mu.MagnitudeUnitName == this.MagnitudeUnitName)
                                ?? throw new NotFoundException(nameof(MagnitudeUnit), $"With name {MagnitudeUnitName}");

            EventHazardUnit eventHazardUnit = new EventHazardUnit()
            {
                Id = this.Id,
                HazardName = this.HazardName,
                MagnitudeUnitId = magnitudeUnit.Id,
                ThresholdValue = this.ThresholdValue,
            };
            return eventHazardUnit;
        }
    }
}
