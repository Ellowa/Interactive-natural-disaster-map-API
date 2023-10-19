namespace InteractiveNaturalDisasterMap.Infrastructure.BackgroundJobs.JsonSchemas
{
    public class EonetEventsGeoJson
    {
        public string Type { get; set; } = "FeatureCollection";

        public List<EonetEventFeature> Features { get; set; } = null!;
    }

    public class EonetEventFeature
    {
        public string Type { get; set; } = "Feature";

        public EventGeometryGeoJson Geometry { get; set; } = null!;

        public EonetEventProperties Properties { get; set; } = null!;
    }

    public class EventGeometryGeoJson
    {
        public string Type { get; set; } = "Point";

        public List<double> Coordinates { get; set; } = null!;
    }

    public class EonetEventProperties
    {
        public string Id { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Link { get; set; }

        public DateTime Date { get; set; }

        public DateTime? Closed { get; set; }

        public double? MagnitudeValue { get; set; }

        public string MagnitudeUnit { get; set; } = string.Empty;

        public List<Category> Categories { get; set; } = null!;

    }

    public class Category
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
