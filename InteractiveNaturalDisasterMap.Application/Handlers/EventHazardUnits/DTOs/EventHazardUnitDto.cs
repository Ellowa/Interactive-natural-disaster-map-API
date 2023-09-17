using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.EventHazardUnits.DTOs
{
    public class EventHazardUnitDto
    {
        public int Id { get; set; }

        public string HazardName { get; set; }

        public string MagnitudeUnitName { get; set; }

        public double? ThresholdValue { get; set; }

        public EventHazardUnitDto(EventHazardUnit eventHazardUnit)
        {
            Id = eventHazardUnit.Id;
            HazardName = eventHazardUnit.HazardName;
            ThresholdValue = eventHazardUnit.ThresholdValue;
            MagnitudeUnitName = eventHazardUnit.MagnitudeUnit.MagnitudeUnitName;
        }

    }
}
