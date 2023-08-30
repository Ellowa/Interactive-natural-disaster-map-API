using System.Reflection;
using InteractiveNaturalDisasterMap.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql
{
    public class InteractiveNaturalDisasterMapDbContext : DbContext
    {
        public InteractiveNaturalDisasterMapDbContext(DbContextOptions<InteractiveNaturalDisasterMapDbContext>  options) : base(options) { }

        public DbSet<NaturalDisasterEvent> NaturalDisasterEvents { get; set; }
        public DbSet<EventCategory> EventsCategories { get; set; }
        public DbSet<EventSource> EventSources { get; set; }
        public DbSet<MagnitudeUnit> MagnitudeUnits { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<EventsCollection> EventsCollections { get; set; }
        public DbSet<EventsCollectionInfo> EventsCollectionsInfo { get; set; }
        public DbSet<UnconfirmedEvent> UnconfirmedEvents { get; set; }
        public DbSet<EventHazardUnit> EventHazardUnits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
