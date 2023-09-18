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

        public string EventCategoryName { get; set; } = string.Empty;

        public string MagnitudeUnitName { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public NaturalDisasterEvent Map(bool confirmed, int eventHazardUnitId, int sourceId, int eventCategoryId, int magnitudeUnitId)
        {
            NaturalDisasterEvent naturalDisasterEvent = new NaturalDisasterEvent()
            {
                Title = this.Title,
                Link = this.Link,
                StartDate = this.StartDate.ToUniversalTime(),
                EndDate = this.EndDate!,
                MagnitudeValue = this.MagnitudeValue,
                Confirmed = confirmed,
                EventCategoryId = eventCategoryId,
                SourceId = sourceId,
                MagnitudeUnitId = magnitudeUnitId,
                EventHazardUnitId = eventHazardUnitId,
                Latitude = Latitude,
                Longitude = Longitude,
            };
            if (naturalDisasterEvent.EndDate != null)
            {
                naturalDisasterEvent.EndDate = ((DateTime)naturalDisasterEvent.EndDate).ToUniversalTime();
            }
            return naturalDisasterEvent;
        }
    }
}
