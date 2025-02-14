﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Spotico.Domain.Database;

#nullable disable

namespace Spotico.Core.Migrations
{
    [DbContext(typeof(SpoticoDbContext))]
    [Migration("20250202145805_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Spotico.Domain.Models.Album", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CoverPath")
                        .HasColumnType("text");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<List<Guid>>("TrackIds")
                        .HasColumnType("uuid[]");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Albums");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7c97edb9-32ee-41fd-abfe-99e83f29ca78"),
                            CoverPath = "C:\\Programming\\ASP.NET+Angular\\Spotico\\Spotico\\Spotico.Server\\wwwroot/images/default.png",
                            CreatedById = new Guid("a4ff30e3-88b2-4688-8811-81ed720f9b5b"),
                            Title = "Through The Disaster",
                            TrackIds = new List<Guid> { new Guid("fa33f69f-190c-4f00-9009-99f1fe4a07a2"), new Guid("d535ae45-e567-4cf5-96a3-a2f6ca95ed62") }
                        });
                });

            modelBuilder.Entity("Spotico.Domain.Models.Playlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(600)");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<List<Guid>>("TrackIds")
                        .HasColumnType("uuid[]");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Playlists");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b9bbf213-5030-4de0-b65d-045fbc1df85d"),
                            CreatedById = new Guid("6aaf1267-83d8-4823-a9d0-677e917067b9"),
                            Description = "The best of Nirvana",
                            IsPublic = false,
                            Title = "Kurtka Cobain",
                            TrackIds = new List<Guid> { new Guid("fa33f69f-190c-4f00-9009-99f1fe4a07a2"), new Guid("d535ae45-e567-4cf5-96a3-a2f6ca95ed62") }
                        });
                });

            modelBuilder.Entity("Spotico.Domain.Models.Track", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AlbumId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ArtistId")
                        .HasColumnType("uuid");

                    b.Property<double>("Duration")
                        .HasColumnType("double precision");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("TrackPath")
                        .HasColumnType("text");

                    b.Property<int>("Views")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("Tracks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("fa33f69f-190c-4f00-9009-99f1fe4a07a2"),
                            AlbumId = new Guid("7c97edb9-32ee-41fd-abfe-99e83f29ca78"),
                            ArtistId = new Guid("a4ff30e3-88b2-4688-8811-81ed720f9b5b"),
                            Duration = 196.30000000000001,
                            Title = "Through The Disaster",
                            TrackPath = "C:\\Programming\\ASP.NET+Angular\\Spotico\\Spotico\\Spotico.Server\\wwwroot/tracks/1.mp3",
                            Views = 51445
                        },
                        new
                        {
                            Id = new Guid("d535ae45-e567-4cf5-96a3-a2f6ca95ed62"),
                            AlbumId = new Guid("7c97edb9-32ee-41fd-abfe-99e83f29ca78"),
                            ArtistId = new Guid("a4ff30e3-88b2-4688-8811-81ed720f9b5b"),
                            Duration = 141.5,
                            Title = "welcome and goodbye",
                            TrackPath = "C:\\Programming\\ASP.NET+Angular\\Spotico\\Spotico\\Spotico.Server\\wwwroot/tracks/2.mp3",
                            Views = 1425272
                        });
                });

            modelBuilder.Entity("Spotico.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Bio")
                        .HasColumnType("VARCHAR(600)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(320)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7697442a-eb32-4f87-a447-ac535bdb97f8"),
                            Bio = "This is the default user.",
                            Email = "user@gmail.com",
                            Password = "user",
                            Role = "User",
                            Username = "user"
                        },
                        new
                        {
                            Id = new Guid("6aaf1267-83d8-4823-a9d0-677e917067b9"),
                            Bio = "This is the admin user.",
                            Email = "admin@gmail.com",
                            Password = "admin",
                            Role = "Admin",
                            Username = "admin"
                        },
                        new
                        {
                            Id = new Guid("a4ff30e3-88b2-4688-8811-81ed720f9b5b"),
                            Bio = "This is the artist.",
                            Email = "artist@gmail.com",
                            Password = "artist",
                            Role = "Author",
                            Username = "artist"
                        });
                });

            modelBuilder.Entity("Spotico.Domain.Models.Album", b =>
                {
                    b.HasOne("Spotico.Domain.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Spotico.Domain.Models.Playlist", b =>
                {
                    b.HasOne("Spotico.Domain.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Spotico.Domain.Models.Track", b =>
                {
                    b.HasOne("Spotico.Domain.Models.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Spotico.Domain.Models.User", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Artist");
                });
#pragma warning restore 612, 618
        }
    }
}
