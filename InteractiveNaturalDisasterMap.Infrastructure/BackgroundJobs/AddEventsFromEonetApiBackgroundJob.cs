using System.Linq.Expressions;
using System.Net.Http.Json;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.CreateNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.Commands.UpdateNaturalDisasterEvent;
using InteractiveNaturalDisasterMap.Application.Handlers.NaturalDisasterEvents.DTOs;
using InteractiveNaturalDisasterMap.Domain.Entities;
using InteractiveNaturalDisasterMap.Infrastructure.BackgroundJobs.JsonSchemas;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;

namespace InteractiveNaturalDisasterMap.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class AddEventsFromEonetApiBackgroundJob : IJob
    {
        private readonly ILogger<AddEventsFromEonetApiBackgroundJob> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMediator _mediator;
        private readonly INaturalDisasterEventRepository _naturalDisasterEventRepository;

        public AddEventsFromEonetApiBackgroundJob(ILogger<AddEventsFromEonetApiBackgroundJob> logger, IHttpClientFactory clientFactory, IMediator mediator, INaturalDisasterEventRepository naturalDisasterEventRepository)
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
                "https://eonet.gsfc.nasa.gov/api/v3/events/geojson?status=all&category=drought,earthquakes,floods,landslides,severeStorms,snow,tempExtremes,volcanoes,wildfires&days=30";
            var eonetEventFeatures = (await client.GetFromJsonAsync<EonetEventsGeoJson>(requestUrl))?.Features.OrderBy(f => f.Properties.Date);

            var eventsAddedCounter = 0;
            var eventsUpdatedCounter = 0;
            if (eonetEventFeatures != null)
            {
                foreach (var eonetEventFeature in eonetEventFeatures)
                {
                    if (eonetEventFeature.Properties.Categories.Count() == 1 && eonetEventFeature.Geometry.Type == "Point")
                    {
                        if (string.IsNullOrEmpty(eonetEventFeature.Properties.MagnitudeUnit))
                            eonetEventFeature.Properties.MagnitudeUnit = "undefined";

                        Expression<Func<NaturalDisasterEvent, bool>> filter = nte => nte.IdInThirdPartyApi == eonetEventFeature.Properties.Id;
                        var naturalDisasterEvent = (await _naturalDisasterEventRepository.GetAllAsync(context.CancellationToken, filter))
                            .FirstOrDefault();
                        if (naturalDisasterEvent == null)
                        {
                            var request = new CreateNaturalDisasterEventRequest()
                            {
                                CreateNaturalDisasterEventDto = new CreateNaturalDisasterEventDto
                                {
                                    Title = eonetEventFeature.Properties.Title,
                                    Link = eonetEventFeature.Properties.Link,
                                    StartDate = eonetEventFeature.Properties.Date,
                                    EndDate = eonetEventFeature.Properties.Closed,
                                    MagnitudeValue = eonetEventFeature.Properties.MagnitudeValue,
                                    EventCategoryName = eonetEventFeature.Properties.Categories.FirstOrDefault()?.Id!,
                                    MagnitudeUnitName = eonetEventFeature.Properties.MagnitudeUnit,
                                    Latitude = eonetEventFeature.Geometry.Coordinates[1],
                                    Longitude = eonetEventFeature.Geometry.Coordinates[0],
                                },
                                SourceName = "nasaEONET",
                                IdInThirdPartyApi = eonetEventFeature.Properties.Id,
                            };
                            try
                            {
                                await _mediator.Send(request);
                                eventsAddedCounter++;
                            }
                            catch (Exception e)
                            {
                                _logger.LogError("Error while adding new events from EONET API. \nMassage:\n" + e.Message);
                            }
                            
                        }
                        else
                        {
                            var request = new UpdateNaturalDisasterEventRequest()
                            {
                                UpdateNaturalDisasterEventDto = new UpdateNaturalDisasterEventDto
                                {
                                    Id = naturalDisasterEvent.Id,
                                    Title = eonetEventFeature.Properties.Title,
                                    Link = eonetEventFeature.Properties.Link,
                                    StartDate = eonetEventFeature.Properties.Date,
                                    EndDate = eonetEventFeature.Properties.Closed,
                                    MagnitudeValue = eonetEventFeature.Properties.MagnitudeValue,
                                    EventCategoryName = eonetEventFeature.Properties.Categories.FirstOrDefault()?.Id!,
                                    MagnitudeUnitName = eonetEventFeature.Properties.MagnitudeUnit,
                                    Latitude = eonetEventFeature.Geometry.Coordinates[1],
                                    Longitude = eonetEventFeature.Geometry.Coordinates[0],
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

            _logger.LogInformation($"Added {eventsAddedCounter} new events and updated {eventsUpdatedCounter} events from EONET API at {DateTime.UtcNow}");
        }
    }
}
