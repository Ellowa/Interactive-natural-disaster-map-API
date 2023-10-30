using System.Reflection;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql
{
    public class InteractiveNaturalDisasterMapDbContext : DbContext
    {
        public InteractiveNaturalDisasterMapDbContext(DbContextOptions<InteractiveNaturalDisasterMapDbContext>  options) : base(options) { }

        public DbSet<NaturalDisasterEvent> NaturalDisasterEvents { get; set; } = null!;
        public DbSet<EventCategory> EventsCategories { get; set; } = null!;
        public DbSet<EventSource> EventSources { get; set; } = null!;
        public DbSet<MagnitudeUnit> MagnitudeUnits { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserRole> UserRoles { get; set; } = null!;
        public DbSet<EventsCollection> EventsCollections { get; set; } = null!;
        public DbSet<EventsCollectionInfo> EventsCollectionsInfo { get; set; } = null!;
        public DbSet<UnconfirmedEvent> UnconfirmedEvents { get; set; } = null!;
        public DbSet<EventHazardUnit> EventHazardUnits { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
