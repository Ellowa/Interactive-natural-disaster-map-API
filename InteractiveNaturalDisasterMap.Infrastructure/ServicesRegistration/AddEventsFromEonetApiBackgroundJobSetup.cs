using InteractiveNaturalDisasterMap.Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Options;
using Quartz;

namespace InteractiveNaturalDisasterMap.Infrastructure.ServicesRegistration
{
    public class AddEventsFromEonetApiBackgroundJobSetup : IConfigureOptions<QuartzOptions>
    {
        public void Configure(QuartzOptions options)
        {
            var eonetApiJobKey = JobKey.Create(nameof(AddEventsFromEonetApiBackgroundJob));
            options
                .AddJob<AddEventsFromEonetApiBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(eonetApiJobKey))
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(eonetApiJobKey)
                        .WithSimpleSchedule(schedule => schedule.WithIntervalInMinutes(60).RepeatForever()));
        }
    }
}
