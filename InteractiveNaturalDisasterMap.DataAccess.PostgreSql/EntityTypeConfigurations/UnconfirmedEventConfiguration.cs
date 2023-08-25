using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.EntityTypeConfigurations
{
    public class UnconfirmedEventConfiguration : IEntityTypeConfiguration<UnconfirmedEvent>
    {
        public void Configure(EntityTypeBuilder<UnconfirmedEvent> builder)
        {
            builder
                .HasKey(ue => ue.EventId);
            builder
                .HasOne(ue => ue.Event)
                .WithOne(e => e.UnconfirmedEvent)
                .HasForeignKey<UnconfirmedEvent>(ue => ue.EventId);
            builder
                .HasOne(ue => ue.User)
                .WithMany(u => u.UnconfirmedEvents)
                .HasForeignKey(ue => ue.UserId);
        }
    }
}
