﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace the_cue_api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240318164551_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("ArtistSong", b =>
                {
                    b.Property<int>("ArtistsId")
                        .HasColumnType("int");

                    b.Property<int>("SongsId")
                        .HasColumnType("int");

                    b.HasKey("ArtistsId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("ArtistSong");
                });

            modelBuilder.Entity("CueItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int?>("RequestId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("RequestId");

                    b.HasIndex("SongId");

                    b.ToTable("CueItem");
                });

            modelBuilder.Entity("Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.Property<int>("GenresGenreId")
                        .HasColumnType("int");

                    b.Property<int>("SongsId")
                        .HasColumnType("int");

                    b.HasKey("GenresGenreId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("GenreSong");
                });

            modelBuilder.Entity("Playlist", b =>
                {
                    b.Property<int>("PlaylistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlaylistId"));

                    b.Property<int?>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int?>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("PlaylistSongId")
                        .HasColumnType("int");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistId");

                    b.HasIndex("ArtistId");

                    b.HasIndex("GenreId");

                    b.ToTable("Playlists");
                });

            modelBuilder.Entity("PlaylistSongs", b =>
                {
                    b.Property<int>("PlaylistSongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlaylistSongId"));

                    b.Property<int>("PlaylistId")
                        .HasColumnType("int");

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.HasKey("PlaylistSongId");

                    b.HasIndex("PlaylistId");

                    b.HasIndex("SongId");

                    b.ToTable("PlaylistSongs");
                });

            modelBuilder.Entity("Request", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestId"));

                    b.Property<int>("SongId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("RequestId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Album")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("AlbumCover")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastPlayed")
                        .HasColumnType("datetime2");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayCount")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("ArtistSong", b =>
                {
                    b.HasOne("Artist", null)
                        .WithMany()
                        .HasForeignKey("ArtistsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CueItem", b =>
                {
                    b.HasOne("Artist", null)
                        .WithMany("CueItems")
                        .HasForeignKey("ArtistId");

                    b.HasOne("Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId");

                    b.HasOne("Song", "Song")
                        .WithMany()
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.HasOne("Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Playlist", b =>
                {
                    b.HasOne("Artist", null)
                        .WithMany("Playlist")
                        .HasForeignKey("ArtistId");

                    b.HasOne("Genre", null)
                        .WithMany("Playlists")
                        .HasForeignKey("GenreId");
                });

            modelBuilder.Entity("PlaylistSongs", b =>
                {
                    b.HasOne("Playlist", "Playlist")
                        .WithMany()
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Song", "Song")
                        .WithMany("PlaylistSongs")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Playlist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("Artist", b =>
                {
                    b.Navigation("CueItems");

                    b.Navigation("Playlist");
                });

            modelBuilder.Entity("Genre", b =>
                {
                    b.Navigation("Playlists");
                });

            modelBuilder.Entity("Song", b =>
                {
                    b.Navigation("PlaylistSongs");
                });
#pragma warning restore 612, 618
        }
    }
}
