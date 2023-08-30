using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs
{
    public class CreateNaturalDisasterEventDto
    {
        public string Title { get; set; } = null!;

        public string? Link { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double? MagnitudeValue { get; set; }

        public bool Confirmed { get; set; } = false;

        public int EventCategoryId { get; set; }

        public int SourceId { get; set; }

        public int MagnitudeUnitId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public NaturalDisasterEvent Map(int eventHazardUnitId, int coordinateId)
        {
            NaturalDisasterEvent naturalDisasterEvent = new NaturalDisasterEvent()
            {
                Title = this.Title,
                Link = this.Link,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                MagnitudeValue = this.MagnitudeValue,
                Confirmed = this.Confirmed,
                EventCategoryId = this.EventCategoryId,
                SourceId = this.SourceId,
                MagnitudeUnitId = this.MagnitudeUnitId,
                EventHazardUnitId = eventHazardUnitId,
                CoordinateId = coordinateId,
            };
            return naturalDisasterEvent;
        }
    }
}
