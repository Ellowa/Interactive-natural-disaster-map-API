﻿using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs
{
    public class UpdateNaturalDisasterEventDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Link { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double? MagnitudeValue { get; set; }

        public int EventCategoryId { get; set; }

        public int MagnitudeUnitId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public NaturalDisasterEvent Map(bool confirmed, int eventHazardUnitId, int coordinateId)
        {
            NaturalDisasterEvent naturalDisasterEvent = new NaturalDisasterEvent()
            {
                Id = this.Id,
                Title = this.Title,
                Link = this.Link,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                MagnitudeValue = this.MagnitudeValue,
                Confirmed = confirmed,
                EventCategoryId = this.EventCategoryId,
                MagnitudeUnitId = this.MagnitudeUnitId,
                EventHazardUnitId = eventHazardUnitId,
                CoordinateId = coordinateId,
            };
            return naturalDisasterEvent;
        }
    }
}
