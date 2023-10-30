using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.EntityTypeConfigurations
{
    internal class MagnitudeUnitConfiguration : IEntityTypeConfiguration<MagnitudeUnit>
    {
        public void Configure(EntityTypeBuilder<MagnitudeUnit> builder)
        {
            builder.HasIndex(mu => mu.MagnitudeUnitName).IsUnique();

            builder
                .HasMany(mu => mu.EventCategories)
                .WithMany(ec => ec.MagnitudeUnits);
        }
    }
}
