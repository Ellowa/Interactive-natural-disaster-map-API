using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.EntityTypeConfigurations
{
    internal class EventSourceConfiguration : IEntityTypeConfiguration<EventSource>
    {
        public void Configure(EntityTypeBuilder<EventSource> builder)
        {
            builder.HasIndex(es => es.SourceType).IsUnique();
        }
    }
}
