namespace Data_Access.Entities
{
    internal class Coordinate : BaseEntity
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public Event Event { get; set; }
    }
}
