using Microsoft.EntityFrameworkCore;
using Profiles.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiles.DAL.Data
{
    public class ProfileDbContext
        : DbContext
    {
        public ProfileDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Connection> Connections { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Profile>()
                .Property(e => e.Phone)
                .HasMaxLength(20);

            modelBuilder.Entity<Connection>()
                .HasOne(x => x.From)
                .WithMany(x => x.ConnectionFroms);

            modelBuilder.Entity<Connection>()
                .HasOne(x => x.To)
                .WithMany(x => x.ConnectionTos)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Profile)
                .WithOne(x => x.User);
        }
    }
}
