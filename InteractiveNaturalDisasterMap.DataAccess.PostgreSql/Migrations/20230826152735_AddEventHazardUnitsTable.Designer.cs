﻿// <auto-generated />
using System;
using InteractiveNaturalDisasterMap.DataAccess.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace InteractiveNaturalDisasterMap.DataAccess.PostgreSql.Migrations
{
    [DbContext(typeof(InteractiveNaturalDisasterMapDbContext))]
    [Migration("20230826152735_AddEventHazardUnitsTable")]
    partial class AddEventHazardUnitsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EventsCategories");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventCoordinate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("EventCoordinates");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventHazardUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("HazardName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MagnitudeUnitId")
                        .HasColumnType("integer");

                    b.Property<double?>("ThresholdValue")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MagnitudeUnitId");

                    b.ToTable("EventHazardUnits");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventSource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("SourceType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EventSources");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventsCollection", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int>("CollectionId")
                        .HasColumnType("integer");

                    b.HasKey("EventId", "CollectionId");

                    b.HasIndex("CollectionId");

                    b.ToTable("EventsCollections");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventsCollectionInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CollectionName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CollectionName")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("EventsCollectionsInfo");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.MagnitudeUnit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("MagnitudeUnitName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MagnitudeUnits");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.NaturalDisasterEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Confirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("CoordinateId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EventCategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("EventHazardUnitId")
                        .HasColumnType("integer");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<int>("MagnitudeUnitId")
                        .HasColumnType("integer");

                    b.Property<double?>("MagnitudeValue")
                        .HasColumnType("double precision");

                    b.Property<int>("SourceId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CoordinateId")
                        .IsUnique();

                    b.HasIndex("EventCategoryId");

                    b.HasIndex("EventHazardUnitId");

                    b.HasIndex("MagnitudeUnitId");

                    b.HasIndex("SourceId");

                    b.ToTable("NaturalDisasterEvents");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.UnconfirmedEvent", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("UnconfirmedEvents");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("JwtRefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.Property<string>("Telegram")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventHazardUnit", b =>
                {
                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.MagnitudeUnit", "MagnitudeUnit")
                        .WithMany("EventHazardUnits")
                        .HasForeignKey("MagnitudeUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MagnitudeUnit");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventsCollection", b =>
                {
                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.EventsCollectionInfo", "EventsCollectionInfo")
                        .WithMany("EventsCollection")
                        .HasForeignKey("CollectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.NaturalDisasterEvent", "Event")
                        .WithMany("EventsCollection")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("EventsCollectionInfo");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventsCollectionInfo", b =>
                {
                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.User", "User")
                        .WithMany("EventsCollectionInfos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.NaturalDisasterEvent", b =>
                {
                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.EventCoordinate", "Coordinate")
                        .WithOne("Event")
                        .HasForeignKey("InteractiveNaturalDisasterMap.Domain.Entities.NaturalDisasterEvent", "CoordinateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.EventCategory", "Category")
                        .WithMany("Events")
                        .HasForeignKey("EventCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.EventHazardUnit", "EventHazardUnit")
                        .WithMany("NaturalDisasterEvents")
                        .HasForeignKey("EventHazardUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.MagnitudeUnit", "MagnitudeUnit")
                        .WithMany("Events")
                        .HasForeignKey("MagnitudeUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.EventSource", "Source")
                        .WithMany("Events")
                        .HasForeignKey("SourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Coordinate");

                    b.Navigation("EventHazardUnit");

                    b.Navigation("MagnitudeUnit");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.UnconfirmedEvent", b =>
                {
                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.NaturalDisasterEvent", "Event")
                        .WithOne("UnconfirmedEvent")
                        .HasForeignKey("InteractiveNaturalDisasterMap.Domain.Entities.UnconfirmedEvent", "EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.User", "User")
                        .WithMany("UnconfirmedEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.User", b =>
                {
                    b.HasOne("InteractiveNaturalDisasterMap.Domain.Entities.UserRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventCategory", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventCoordinate", b =>
                {
                    b.Navigation("Event")
                        .IsRequired();
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventHazardUnit", b =>
                {
                    b.Navigation("NaturalDisasterEvents");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventSource", b =>
                {
                    b.Navigation("Events");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.EventsCollectionInfo", b =>
                {
                    b.Navigation("EventsCollection");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.MagnitudeUnit", b =>
                {
                    b.Navigation("EventHazardUnits");

                    b.Navigation("Events");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.NaturalDisasterEvent", b =>
                {
                    b.Navigation("EventsCollection");

                    b.Navigation("UnconfirmedEvent")
                        .IsRequired();
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.User", b =>
                {
                    b.Navigation("EventsCollectionInfos");

                    b.Navigation("UnconfirmedEvents");
                });

            modelBuilder.Entity("InteractiveNaturalDisasterMap.Domain.Entities.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
