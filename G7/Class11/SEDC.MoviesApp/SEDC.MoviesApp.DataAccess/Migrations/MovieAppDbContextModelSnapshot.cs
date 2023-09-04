﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEDC.MoviesApp.DataAccess;

#nullable disable

namespace SEDC.MoviesApp.DataAccess.Migrations
{
    [DbContext(typeof(MovieAppDbContext))]
    partial class MovieAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SEDC.MoviesApp.Domain.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Documentary",
                            Genre = 4,
                            Title = "Oppenheimer",
                            Year = 2023
                        },
                        new
                        {
                            Id = 2,
                            Description = "Girl",
                            Genre = 1,
                            Title = "Barbie",
                            Year = 2023
                        },
                        new
                        {
                            Id = 3,
                            Description = "Action movie",
                            Genre = 2,
                            Title = "Top Gun:Maverick",
                            Year = 2021
                        },
                        new
                        {
                            Id = 4,
                            Description = "Action movie",
                            Genre = 1,
                            Title = "Dumb and dumber",
                            Year = 1994
                        });
                });
#pragma warning restore 612, 618
        }
    }
}