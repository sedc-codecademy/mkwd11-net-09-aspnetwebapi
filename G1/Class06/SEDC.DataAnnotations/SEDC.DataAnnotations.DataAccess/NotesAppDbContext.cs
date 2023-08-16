using Microsoft.EntityFrameworkCore;
using SEDC.DataAnnotations.Domain.Models;

namespace SEDC.DataAnnotations.DataAccess
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
