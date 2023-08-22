namespace InteractiveNaturalDisasterMap.Entities
{
    public class EventCoordinate : BaseEntity
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public NaturalDisasterEvent Event { get; set; } = null!;
    }
}
