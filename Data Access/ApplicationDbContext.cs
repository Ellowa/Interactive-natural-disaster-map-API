﻿using Data_Access.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_Access
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>  options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventsCategories { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<MagnitudeUnit> MagnitudeUnits { get; set; }
        public DbSet<Coordinate> Coordinates { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<EventsCollection> EventsCollections { get; set; }
        public DbSet<EventsCollectionInfo> EventsCollectionsInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.EventCategoryId);
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Source)
                .WithMany(s => s.Events)
                .HasForeignKey(e => e.SourceId);
            modelBuilder.Entity<Event>()
                .HasOne(e => e.MagnitudeUnit)
                .WithMany(m => m.Events)
                .HasForeignKey(e => e.MagnitudeUnitId);
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Coordinate)
                .WithOne(c => c.Event)
                .HasForeignKey<Event>(e => e.CoordinateId);
            modelBuilder.Entity<Event>()
                .HasOne(e => e.User)
                .WithMany(u => u.Events)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<EventsCollection>()
                .HasKey(ec => new { ec.EventId, ec.UserId, ec.CollectionId });
            modelBuilder.Entity<EventsCollection>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.EventsCollection)
                .HasForeignKey(ec => ec.EventId);
            modelBuilder.Entity<EventsCollection>()
                .HasOne(ec => ec.User)
                .WithMany(u => u.EventsCollection)
                .HasForeignKey(ec => ec.UserId);
            modelBuilder.Entity<EventsCollection>()
                .HasOne(ec => ec.EventsCollectionInfo)
                .WithMany(eci => eci.EventsCollection)
                .HasForeignKey(ec => ec.CollectionId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(ur => ur.Users)
                .HasForeignKey(u => u.RoleId);
        }
    }
}
