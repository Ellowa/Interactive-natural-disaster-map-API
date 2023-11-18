using InteractiveNaturalDisasterMap.DataAccess.PostgreSql;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers
{
    public class IntegrationTestsWebAppFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<InteractiveNaturalDisasterMapDbContext>));
                if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

                var quartzHostedServiceDescriptor = services.FirstOrDefault(descriptor => descriptor.ImplementationType == typeof(QuartzHostedService));
                if (quartzHostedServiceDescriptor != null)
                {
                    services.Remove(quartzHostedServiceDescriptor);
                }

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();
                services.AddDbContext<InteractiveNaturalDisasterMapDbContext>(options =>
                {
                    options.UseInMemoryDatabase(databaseName: Guid.Empty.ToString());
                    options.UseInternalServiceProvider(serviceProvider);
                });
            });
        }
    }
}
