﻿namespace InteractiveNaturalDisasterMap.Domain.Entities
{
    public class MagnitudeUnit : BaseEntity
    {
        public string MagnitudeUnitName { get; set; } = null!;

        public string MagnitudeUnitDescription { get; set; } = null!;

        public ICollection<NaturalDisasterEvent> Events { get; set; } = null!;

        public ICollection<EventHazardUnit> EventHazardUnits { get; set; } = null!;

        public ICollection<EventCategory> EventCategories { get; set; } = null!;
    }
}
