﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using InteractiveNaturalDisasterMap.Application.Behaviors;


namespace InteractiveNaturalDisasterMap.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
