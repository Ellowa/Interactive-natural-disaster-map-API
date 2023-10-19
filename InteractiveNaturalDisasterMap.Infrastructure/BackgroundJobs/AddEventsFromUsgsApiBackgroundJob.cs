using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.CreateNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.UpdateNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using InteractiveNaturalDisasterMap.Infrastructure.BackgroundJobs.JsonSchemas;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Linq.Expressions;
using System.Net.Http.Json;

namespace InteractiveNaturalDisasterMap.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class AddEventsFromUsgsApiBackgroundJob : IJob
    {
        private readonly ILogger<AddEventsFromUsgsApiBackgroundJob> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMediator _mediator;
        private readonly INaturalDisasterEventRepository _naturalDisasterEventRepository;

        public AddEventsFromUsgsApiBackgroundJob(ILogger<AddEventsFromUsgsApiBackgroundJob> logger, IHttpClientFactory clientFactory, IMediator mediator, INaturalDisasterEventRepository naturalDisasterEventRepository)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _mediator = mediator;
            _naturalDisasterEventRepository = naturalDisasterEventRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var client = _clientFactory.CreateClient();
            string requestUrl =
                "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/4.5_hour.geojson";
            var usgsEventFeatures = (await client.GetFromJsonAsync<UsgsEventsGeoJson>(requestUrl))?.Features.OrderBy(f => f.Properties.Time);

            var eventsAddedCounter = 0;
            var eventsUpdatedCounter = 0;
            if (usgsEventFeatures != null)
            {
                foreach (var usgsEventFeature in usgsEventFeatures)
                {
                    if (usgsEventFeature.Geometry.Type == "Point")
                    {
                        DateTime epochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                        DateTime? endDate = null;
                        if (usgsEventFeature.Properties.Updated != null)
                            endDate = epochDateTime.AddMilliseconds((long)usgsEventFeature.Properties.Updated);

                        Expression<Func<NaturalDisasterEvent, bool>> filter = nte => nte.IdInThirdPartyApi == usgsEventFeature.Properties.Code;
                        var naturalDisasterEvent = (await _naturalDisasterEventRepository.GetAllAsync(context.CancellationToken, filter))
                            .FirstOrDefault();
                        if (naturalDisasterEvent == null)
                        {
                            var request = new CreateNaturalDisasterEventRequest()
                            {
                                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto
                                {
                                    Title = usgsEventFeature.Properties.Title,
                                    Link = usgsEventFeature.Properties.Url,
                                    StartDate = epochDateTime.AddMilliseconds(usgsEventFeature.Properties.Time),
                                    EndDate = endDate,
                                    MagnitudeValue = usgsEventFeature.Properties.Mag,
                                    EventCategoryName = "earthquakes",
                                    MagnitudeUnitName = "earthquakeMagnitude",
                                    Latitude = usgsEventFeature.Geometry.Coordinates[1],
                                    Longitude = usgsEventFeature.Geometry.Coordinates[0],
                                },
                                SourceName = "usgsEarthquake",
                                IdInThirdPartyApi = usgsEventFeature.Properties.Code,
                            };
                            try
                            {
                                await _mediator.Send(request);
                                eventsAddedCounter++;
                            }
                            catch (Exception e)
                            {
                                _logger.LogError("Error while adding new events from USGS API. \nMassage:\n" + e.Message);
                            }

                        }
                        else
                        {
                            var request = new UpdateNaturalDisasterEventRequest()
                            {
                                UpdateNaturalDisasterEventDto = new UpdateNaturalDisasterEventDto
                                {
                                    Id = naturalDisasterEvent.Id,
                                    Title = usgsEventFeature.Properties.Title,
                                    Link = usgsEventFeature.Properties.Url,
                                    StartDate = epochDateTime.AddMilliseconds(usgsEventFeature.Properties.Time),
                                    EndDate = endDate,
                                    MagnitudeValue = usgsEventFeature.Properties.Mag,
                                    EventCategoryName = "earthquakes",
                                    MagnitudeUnitName = "earthquakeMagnitude",
                                    Latitude = usgsEventFeature.Geometry.Coordinates[1],
                                    Longitude = usgsEventFeature.Geometry.Coordinates[0],
                                },
                            };
                            try
                            {
                                await _mediator.Send(request);
                                eventsUpdatedCounter++;
                            }
                            catch (Exception e)
                            {
                                _logger.LogError(e.Message);
                            }

                        }
                    }
                }
            }

            _logger.LogInformation($"Added {eventsAddedCounter} new events and updated {eventsUpdatedCounter} events from USGS API at {DateTime.UtcNow}");
        }
    }
}
