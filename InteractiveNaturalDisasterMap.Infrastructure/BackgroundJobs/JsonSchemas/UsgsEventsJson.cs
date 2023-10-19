namespace InteractiveNaturalDisasterMap.Infrastructure.BackgroundJobs.JsonSchemas
{
    public class UsgsEventsGeoJson
    {
        public string Type { get; set; } = "FeatureCollection";

        public List<UsgsEventFeature> Features { get; set; } = null!;
    }

    public class UsgsEventFeature
    {
        public string Type { get; set; } = "Feature";

        public EventGeometryGeoJson Geometry { get; set; } = null!;

        public UsgsEventProperties Properties { get; set; } = null!;
    }

    public class UsgsEventProperties
    {
        public string Code { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Url { get; set; }

        public long Time { get; set; }

        public long? Updated { get; set; }

        public double? Mag { get; set; }
    }
}
