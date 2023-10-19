using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.EntityTypeConfigurations
{
    public class NaturalDisasterEventConfiguration : IEntityTypeConfiguration<NaturalDisasterEvent>
    {
        public void Configure(EntityTypeBuilder<NaturalDisasterEvent> builder)
        {
            builder
                .HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.EventCategoryId);
            builder
                .HasOne(e => e.Source)
                .WithMany(s => s.Events)
                .HasForeignKey(e => e.SourceId);
            builder
                .HasOne(e => e.MagnitudeUnit)
                .WithMany(m => m.Events)
                .HasForeignKey(e => e.MagnitudeUnitId);

            builder.HasIndex(e => e.IdInThirdPartyApi).IsUnique();
        }
    }
}
