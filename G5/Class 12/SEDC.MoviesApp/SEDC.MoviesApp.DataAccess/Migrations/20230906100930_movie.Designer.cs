﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEDC.MoviesApp.DataAccess;

#nullable disable

namespace SEDC.MoviesApp.DataAccess.Migrations
{
    [DbContext(typeof(MoviesDbContext))]
    [Migration("20230906100930_movie")]
    partial class movie
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SEDC.MoviesApp.Domain.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Bames returns for one last mission to save the president from impending doom.",
                            Genre = 2,
                            Title = "Bames Jond 2",
                            Year = 1970
                        },
                        new
                        {
                            Id = 2,
                            Description = "Wellsa was a failed cryogenic scientist, destined to unfreeze people that have been frozen.",
                            Genre = 4,
                            Title = "Unfrozen",
                            Year = 2020
                        });
                });
#pragma warning restore 612, 618
        }
    }
}