using InteractiveNaturalDisasterMap.DataAccess.PostgreSql;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers
{
    public class BaseIntegrationTest
    {
        private IntegrationTestsWebAppFactory _factory;
        protected InteractiveNaturalDisasterMapDbContext DbContext;
        protected IMediator Mediator;

        [SetUp]
        public void Setup()
        {
            _factory = new IntegrationTestsWebAppFactory();
            var scope = _factory.Services.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<InteractiveNaturalDisasterMapDbContext>();
            Mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        }

        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
        }

    }
}
