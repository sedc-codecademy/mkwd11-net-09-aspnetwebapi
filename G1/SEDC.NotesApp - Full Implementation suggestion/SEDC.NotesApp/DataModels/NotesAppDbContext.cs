using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace DataModels
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions options)
        : base(options) { }

        public DbSet<UserDto> Users { get; set; }
        public DbSet<NoteDto> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NoteDto>()
                .HasOne(x => x.User)
                .WithMany(x => x.NoteList)
                .HasForeignKey(x => x.UserId);

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("123456sedc"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            modelBuilder.Entity<UserDto>()
                .HasData(
                new UserDto()
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    Username = "bob007",
                    Password = hashedPassword
                });
            modelBuilder.Entity<NoteDto>()
                .HasData(
                new NoteDto()
                {
                    Id = 1,
                    Text = "Buy Juice",
                    Color = "blue",
                    Tag = 4,
                    UserId = 1
                },
                new NoteDto()
                {
                    Id = 2,
                    Text = "Learn ASP.NET Core WebApi",
                    Color = "orange",
                    Tag = 1,
                    UserId = 1
                }
                );
        }
    }
}
