using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.EntityTypeConfigurations
{
    public class EventsCollectionInfoConfiguration : IEntityTypeConfiguration<EventsCollectionInfo>
    {
        public void Configure(EntityTypeBuilder<EventsCollectionInfo> builder)
        {
            builder
                .HasOne(eci => eci.User)
                .WithMany(u => u.EventsCollectionInfos)
                .HasForeignKey(eci => eci.UserId);
            builder
                .HasMany(eci => eci.EventsCollection)
                .WithOne(ec => ec.EventsCollectionInfo)
                .HasForeignKey(ec => ec.CollectionId);
            builder.HasIndex(eci => new{ eci.CollectionName, eci.UserId}).IsUnique();
        }
    }
}
