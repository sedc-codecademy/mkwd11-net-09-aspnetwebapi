using Microsoft.EntityFrameworkCore;
using SEDC.NotesAppDA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesAppDA.DataAccess
{
    public class NotesAppDataAnotationDBContext : DbContext
    {
        public NotesAppDataAnotationDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
