using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.AdoNetRepositories;
using SEDC.NotesApp.DataAccess.DapperRepositories;
using SEDC.NotesApp.DataAccess.EFRepositories;
using SEDC.NotesApp.DataAccess.Interfaces;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Implementations;
using SEDC.NotesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<NotesAppDbContext>(x=>x.UseSqlServer("Server=SKL-TANJA-STOJA\\SQLEXPRESS;Database=NotesAppDb;Trusted_Connection=True;TrustServerCertificate=True"));
            services.AddDbContext<NotesAppDbContext>(x=>x.UseSqlServer(connectionString));
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IUserService, UserService>();
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NoteRepository>();
            //services.AddTransient<IRepository<Note>, NoteAdoRepository>();
            //services.AddTransient<IRepository<Note>, NoteDapperRepository>();
            //services.AddTransient<IRepository<User>, UserRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
