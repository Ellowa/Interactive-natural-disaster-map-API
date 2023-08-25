using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InteractiveNaturalDisasterMap.Domain.Entities;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.EntityTypeConfigurations
{
    public class EventsCollectionConfiguration : IEntityTypeConfiguration<EventsCollection>
    {
        public void Configure(EntityTypeBuilder<EventsCollection> builder)
        {
            builder
                .HasKey(ec => new { ec.EventId, ec.CollectionId });
            builder
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventsCollection)
                .HasForeignKey(ec => ec.EventId);
            builder
                .HasOne(ec => ec.EventsCollectionInfo)
                .WithMany(eci => eci.EventsCollection)
                .HasForeignKey(ec => ec.CollectionId);
        }
    }
}
