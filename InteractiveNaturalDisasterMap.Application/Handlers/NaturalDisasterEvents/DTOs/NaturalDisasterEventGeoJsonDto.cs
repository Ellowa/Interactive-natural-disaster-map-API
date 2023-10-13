namespace InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs
{
    public class NaturalDisasterEventGeoJsonDto
    {
        public string Type { get; set; } = "FeatureCollection";

        public List<NaturalDisasterEventFeaturesGeoJson> Features { get; set; }

        public NaturalDisasterEventGeoJsonDto(params NaturalDisasterEventDto[] naturalDisasterEventDtos)
        {
            Features = new List<NaturalDisasterEventFeaturesGeoJson>();
            foreach (var naturalDisasterEventDto in naturalDisasterEventDtos)
            {
                NaturalDisasterEventFeaturesGeoJson feature = new NaturalDisasterEventFeaturesGeoJson()
                {
                    Geometry = new NaturalDisasterEventGeometryGeoJson()
                    {
                        Coordinates = new []{ naturalDisasterEventDto.Longitude, naturalDisasterEventDto.Latitude},
                    },
                    Properties = new NaturalDisasterEventPropertiesGeoJson()
                    {
                        Id = naturalDisasterEventDto.Id,
                        Title = naturalDisasterEventDto.Title,
                        Link = naturalDisasterEventDto.Link,
                        StartDate = naturalDisasterEventDto.StartDate,
                        EndDate = naturalDisasterEventDto.EndDate,
                        Closed = naturalDisasterEventDto.Closed,
                        MagnitudeValue = naturalDisasterEventDto.MagnitudeValue,
                        Confirmed = naturalDisasterEventDto.Confirmed,
                        Category = naturalDisasterEventDto.Category,
                        Source = naturalDisasterEventDto.Source,
                        MagnitudeUnit = naturalDisasterEventDto.MagnitudeUnit,
                        EventHazardUnit = naturalDisasterEventDto.EventHazardUnit,
                    },
                };
                Features.Add(feature);
            }
        }
    }

    public class NaturalDisasterEventFeaturesGeoJson
    {
        public string Type { get; set; } = "Feature";

        public NaturalDisasterEventGeometryGeoJson Geometry { get; set; } = null!;

        public NaturalDisasterEventPropertiesGeoJson Properties { get; set; } = null!;
    }

    public class NaturalDisasterEventGeometryGeoJson
    {
        public string Type { get; set; } = "Point";

        public double[] Coordinates { get; set; } = new double[2];
    }

    public class NaturalDisasterEventPropertiesGeoJson
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Link { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool Closed { get; set; }

        public double? MagnitudeValue { get; set; }

        public bool Confirmed { get; set; }

        public string Category { get; set; } = string.Empty;

        public string Source { get; set; } = string.Empty;

        public string MagnitudeUnit { get; set; } = string.Empty;

        public string EventHazardUnit { get; set; } = string.Empty;

    }
}
