using InteractiveNaturalDisasterMap.Entities;
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
        public DbSet<EventCoordinate> EventCoordinates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<EventsCollection> EventsCollections { get; set; }
        public DbSet<EventsCollectionInfo> EventsCollectionsInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NaturalDisasterEvent>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.EventCategoryId);
            modelBuilder.Entity<NaturalDisasterEvent>()
                .HasOne(e => e.Source)
                .WithMany(s => s.Events)
                .HasForeignKey(e => e.SourceId);
            modelBuilder.Entity<NaturalDisasterEvent>()
                .HasOne(e => e.MagnitudeUnit)
                .WithMany(m => m.Events)
                .HasForeignKey(e => e.MagnitudeUnitId);
            modelBuilder.Entity<NaturalDisasterEvent>()
                .HasOne(e => e.Coordinate)
                .WithOne(c => c.Event)
                .HasForeignKey<NaturalDisasterEvent>(e => e.CoordinateId);

            modelBuilder.Entity<EventsCollection>()
                .HasKey(ec => new { ec.EventId, ec.CollectionId });
            modelBuilder.Entity<EventsCollection>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventsCollection)
                .HasForeignKey(ec => ec.EventId);
            modelBuilder.Entity<EventsCollection>()
                .HasOne(ec => ec.EventsCollectionInfo)
                .WithMany(eci => eci.EventsCollection)
                .HasForeignKey(ec => ec.CollectionId);

            modelBuilder.Entity<EventsCollectionInfo>()
                .HasOne(eci => eci.User)
                .WithMany(u => u.EventsCollectionInfos)
                .HasForeignKey(eci => eci.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(ur => ur.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
