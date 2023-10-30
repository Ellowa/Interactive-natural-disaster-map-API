using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.EntityTypeConfigurations
{
    internal class EventCategoryConfiguration : IEntityTypeConfiguration<EventCategory>
    {
        public void Configure(EntityTypeBuilder<EventCategory> builder)
        {
            builder.HasIndex(ec => ec.CategoryName).IsUnique();

            builder
                .HasMany(ec => ec.MagnitudeUnits)
                .WithMany(mu => mu.EventCategories);
        }
    }
}
