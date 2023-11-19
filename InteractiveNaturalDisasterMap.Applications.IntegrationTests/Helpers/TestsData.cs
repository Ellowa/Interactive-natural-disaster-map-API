using InteractiveNaturalDisasterMap.Application.Utilities;
using InteractiveNaturalDisasterMap.DataAccess.PostgreSql;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.Applications.IntegrationTests.Helpers
{
    internal class TestsData
    {
        public static void SeedData(InteractiveNaturalDisasterMapDbContext context)
        {
            context.EventSources.Add(new EventSource { SourceType = EntityNamesByDefault.DefaultEventSource });

            var magnitudeUnit = new MagnitudeUnit
                { MagnitudeUnitName = EntityNamesByDefault.DefaultMagnitudeUnit, MagnitudeUnitDescription = "Description" };
            context.MagnitudeUnits.Add(magnitudeUnit);

            context.EventHazardUnits.Add(new EventHazardUnit
                { HazardName = EntityNamesByDefault.DefaultEventHazardUnit, MagnitudeUnit = magnitudeUnit });

            context.EventsCategories.Add(new EventCategory()
                { CategoryName = EntityNamesByDefault.DefaultEventCategory, MagnitudeUnits = new List<MagnitudeUnit>() { magnitudeUnit } });

            context.UserRoles.Add(new UserRole() { RoleName = "user" });

            context.SaveChanges();
        }
    }
}
