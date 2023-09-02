using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotesAndTagsApp.DataAccess;
using NotesAndTagsApp.DataAccess.Implementations;
using NotesAndTagsApp.DataAccess.Interfaces;
using NotesAndTagsApp.Domain.Models;
using NotesAndTagsApp.Services.Implementation;
using NotesAndTagsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesAndTagsApp.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<NotesAndTagsAppDbContext>(x =>
            //x.UseSqlServer("Server=.\\SQLExpress;Database = NotesAppDb;Trusted_Connection=True; TrustServerCertificate = True"));
            x.UseSqlServer("Server=.;Database = NotesAppDb;Trusted_Connection=True;TrustServerCertificate = True"));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NoteRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<INoteService, NoteService>();
        }
    }
}
