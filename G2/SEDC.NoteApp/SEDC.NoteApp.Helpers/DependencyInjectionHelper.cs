using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NoteApp.DataAccess;
using SEDC.NoteApp.DataAccess.Abstraction;
using SEDC.NoteApp.DataAccess.AdoNetImplementation;
using SEDC.NoteApp.DataAccess.DapperImplementation;
using SEDC.NoteApp.DataAccess.EntityImplementation;
using SEDC.NoteApp.Domain.Models;
using SEDC.NoteApp.Services.Abstraction;
using SEDC.NoteApp.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NoteApp.Helpers
{
    // Microsoft.EntityFrameworkCore
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NoteAppDbContext>(options => 
                options.UseSqlServer(connectionString));
        }

        public static void RegisterRepositories(this IServiceCollection services) 
        {
            // Dapper Implementation
            //services.AddTransient<IRepository<Note>, NoteDapperRepository>();
            // AdoNet Implementation
            //services.AddTransient<IRepository<Note>, NoteAdoNetRepository>();
            // EntityFramework Implementation
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        public static void RegisterServices(this IServiceCollection services) 
        {
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
