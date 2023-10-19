using InteractiveNaturalDisasterMap.Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace InteractiveNaturalDisasterMap.Infrastructure.ServicesRegistration
{
    public class AddEventsFromUsgsApiBackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var usgsApiJobKey = JobKey.Create(nameof(AddEventsFromUsgsApiBackgroundJob));
            options
                .AddJob<AddEventsFromUsgsApiBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(usgsApiJobKey))
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(usgsApiJobKey)
                        .WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(60).RepeatForever()));
        }
    }
}
