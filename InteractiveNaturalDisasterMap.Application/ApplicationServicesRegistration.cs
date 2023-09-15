using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using InteractiveNaturalDisasterMap.Application.Interfaces;
using InteractiveNaturalDisasterMap.Application.Utilities;


namespace InteractiveNaturalDisasterMap.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            return services;
        }
    }
}
