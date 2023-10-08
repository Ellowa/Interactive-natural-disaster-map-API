using EntityFramework.Exceptions.PostgreSQL;
using InteractiveNaturalDisasterMap.Application.DataAccessInterfaces;
using InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Repositories;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql
{
    public static class DataAccessPostgreSqlServicesRegistration
    {
        public static IServiceCollection ConfigureDataAccessPostgreSqlServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSQLConnection");

            services.AddDbContext<InteractiveNaturalDisasterMapDbContext>(options =>
                options
                    .UseNpgsql(connectionString)
                    .UseExceptionProcessor());

            services.AddScoped<IGenericBaseEntityRepository<EventCategory>, GenericBaseEntityRepository<EventCategory>>();
            services.AddScoped<IGenericBaseEntityRepository<EventSource>, GenericBaseEntityRepository<EventSource>>();
            services.AddScoped<IGenericBaseEntityRepository<MagnitudeUnit>, GenericBaseEntityRepository<MagnitudeUnit>>();
            services.AddScoped<IGenericBaseEntityRepository<EventsCollectionInfo>, GenericBaseEntityRepository<EventsCollectionInfo>>();
            services.AddScoped<IGenericBaseEntityRepository<User>, GenericBaseEntityRepository<User>>();
            services.AddScoped<IGenericBaseEntityRepository<UserRole>, GenericBaseEntityRepository<UserRole>>();
            services.AddScoped<IGenericBaseEntityRepository<EventHazardUnit>, GenericBaseEntityRepository<EventHazardUnit>>();
            services.AddScoped<IEventsCollectionRepository, EventsCollectionRepository>();
            services.AddScoped<IUnconfirmedEventRepository, UnconfirmedEventRepository>();
            services.AddScoped<INaturalDisasterEventRepository, NaturalDisasterEventRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
