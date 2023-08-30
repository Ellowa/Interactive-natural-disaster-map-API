using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs
{
    public class NaturalDisasterEventDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Link { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Closed { get; set; }

        public double? MagnitudeValue { get; set; }

        public bool Confirmed { get; set; }

        public string Category { get; set; }

        public string Source { get; set; }

        public string MagnitudeUnit { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string EventHazardUnit { get; set; }

        public NaturalDisasterEventDto(NaturalDisasterEvent naturalDisasterEvent)
        {
            Id = naturalDisasterEvent.Id;
            Title = naturalDisasterEvent.Title;
            Link = naturalDisasterEvent.Link;
            StartDate = naturalDisasterEvent.StartDate;
            EndDate = naturalDisasterEvent.EndDate;
            Closed = naturalDisasterEvent.EndDate == null ? Closed = false : Closed = true;
            MagnitudeValue = naturalDisasterEvent.MagnitudeValue;
            Confirmed = naturalDisasterEvent.Confirmed;
            Category = naturalDisasterEvent.Category.CategoryName;
            Source = naturalDisasterEvent.Source.SourceType;
            MagnitudeUnit = naturalDisasterEvent.MagnitudeUnit.MagnitudeUnitName;
            Latitude = naturalDisasterEvent.Latitude;
            Longitude = naturalDisasterEvent.Longitude;
            EventHazardUnit = naturalDisasterEvent.EventHazardUnit.HazardName;
        }
    }
}
