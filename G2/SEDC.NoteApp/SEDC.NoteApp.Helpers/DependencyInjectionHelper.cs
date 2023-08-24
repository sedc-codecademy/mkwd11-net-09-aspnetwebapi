using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NoteApp.DataAccess;
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
            services.AddTransient<IRepository<Note>, NoteRepository>();
            services.AddTransient<IRepository<User>, UserRepository>();
        }
    }
}
