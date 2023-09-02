using Microsoft.EntityFrameworkCore;
using NotesAppG5DataAnnotations.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAppG5DataAnnotations.DataAccess
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions options) : base(options) { }  

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
