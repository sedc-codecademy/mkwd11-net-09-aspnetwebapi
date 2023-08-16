using Microsoft.EntityFrameworkCore;
using Movies.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Context
{
    public class MoviesDbContext
        : DbContext
    {
        public MoviesDbContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .Property(x => x.Title)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
