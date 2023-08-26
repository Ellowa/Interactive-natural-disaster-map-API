
using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.EntityTypeConfigurations
{
    public class EventHazardUnitConfiguration : IEntityTypeConfiguration<EventHazardUnit>
    {
        public void Configure(EntityTypeBuilder<EventHazardUnit> builder)
        {
            builder
                .HasOne(ehu => ehu.MagnitudeUnit)
                .WithMany(mu => mu.EventHazardUnits)
                .HasForeignKey(ehu => ehu.MagnitudeUnitId);
            builder
                .HasMany(ehu => ehu.NaturalDisasterEvents)
                .WithOne(nte => nte.EventHazardUnit)
                .HasForeignKey(nte => nte.EventHazardUnitId);
        }
    }
}
