﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEDC.NotesApp.DataAccess;

#nullable disable

namespace SEDC.NotesApp.DataAccess.Migrations
{
    [DbContext(typeof(NotesAppDbContext))]
    partial class NotesAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SEDC.NotesApp.Domain.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Tag")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Priority = 3,
                            Tag = 2,
                            Text = "Buy groceries",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Priority = 2,
                            Tag = 1,
                            Text = "Finish project report",
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Priority = 1,
                            Tag = 3,
                            Text = "Call friends",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("SEDC.NotesApp.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "John",
                            LastName = "Doe",
                            Username = "john_doe"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Jane",
                            LastName = "Smith",
                            Username = "jane_smith"
                        });
                });

            modelBuilder.Entity("SEDC.NotesApp.Domain.Models.Note", b =>
                {
                    b.HasOne("SEDC.NotesApp.Domain.Models.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SEDC.NotesApp.Domain.Models.User", b =>
                {
                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
