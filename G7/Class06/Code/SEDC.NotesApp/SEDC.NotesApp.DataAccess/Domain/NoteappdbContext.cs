using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.DataAccess.Domain.Enums;

namespace SEDC.NotesApp.DataAccess.Domain;

public partial class NoteappdbContext : DbContext
{
    public NoteappdbContext()
    {
    }

    public NoteappdbContext(DbContextOptions<NoteappdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=noteappdb;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notes__3214EC078D51A04F");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Text).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Notes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notes_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07E0C6DE35");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Dragan",
                LastName = "Manaskov",
                UserName = "dmanaskov",
                Age = 27
            }
            );

        modelBuilder.Entity<Note>().HasData(
        new Note
        {
            Id = 1,
            Title = "Work",
            Text = "go to work",
            Priority = PriorityEnum.High,
            Tag = TagEnum.Work,
            UserId = 1
        }
        );


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
