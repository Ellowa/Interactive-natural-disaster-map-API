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

        public double? ThresholdValue { get; set; } // required but can be null
    }
}
