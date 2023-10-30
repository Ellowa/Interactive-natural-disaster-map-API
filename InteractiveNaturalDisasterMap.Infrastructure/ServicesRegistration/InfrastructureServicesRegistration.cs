using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using InteractiveNaturalDisasterMap.Application.InfrastructureInterfaces;
using InteractiveNaturalDisasterMap.Infrastructure.Authentication;
using InteractiveNaturalDisasterMap.Infrastructure.Authorization;
using Quartz;

namespace InteractiveNaturalDisasterMap.Infrastructure.ServicesRegistration
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.ConfigureOptions<JwtOptionsSetup>();

            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.AddScoped<IAuthorizationService, AuthorizationService>();

            services.AddQuartz();

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            services.ConfigureOptions<AddEventsFromEonetApiBackgroundJobSetup>();
            services.ConfigureOptions<AddEventsFromUsgsApiBackgroundJobSetup>();
            return services;
        }
    }
}
