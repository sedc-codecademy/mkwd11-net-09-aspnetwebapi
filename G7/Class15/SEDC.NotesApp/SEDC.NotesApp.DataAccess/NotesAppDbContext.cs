using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.Domain.Enums;
using SEDC.NotesApp.Domain.Models;
using System.Text;
using XSystem.Security.Cryptography;

namespace SEDC.NotesApp.DataAccess
{
    public class NotesAppDbContext : DbContext
    {
        public NotesAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                //we select which property whe want do add some constraints 
                .Property(x => x.FirstName)
                // we add those constraints here
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Age)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(x => x.Username)
                .HasMaxLength(100);

            modelBuilder.Entity<Note>()
                .HasOne(note => note.User)
                .WithMany(user => user.Notes)
                .HasForeignKey(note => note.UserId);

            modelBuilder.Entity<Note>().Property(x => x.Text).HasMaxLength(100);

            //var md5 = new MD5CryptoServiceProvider();
            //var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("admin"));
            //var hashedPAssword = Encoding.ASCII.GetString(md5data);

            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();

            byte[] passwordBytes = Encoding.ASCII.GetBytes("admin");

            byte[] hash = md5CryptoServiceProvider.ComputeHash(passwordBytes);

            string stringHash = Convert.ToHexString(hash);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "admin",
                    Age = 23,
                    Password = stringHash,
                    Role = RoleEnum.Admin
                }
            );

            //modelBuilder.Entity<Note>().HasData(
            //        new Note
            //        {
            //            Id = 1,
            //            Priority = PriorityEnum.High,
            //            Tag = TagEnum.Work,
            //            Text = "Go to work",
            //            UserId = 1
            //        }
            //    );
        }
    }
}
