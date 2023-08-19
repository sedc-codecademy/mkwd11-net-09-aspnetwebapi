namespace SEDC.NotesAppFinal.Helpers
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SEDC.NotesAppFinal.DataAccess;
    using SEDC.NotesAppFinal.DataAccess.Implementations;
    using SEDC.NotesAppFinal.DataAccess.Interfaces;
    using SEDC.NotesAppFinal.Domain.Models;
    using SEDC.NotesAppFinal.Services.Implementations;
    using SEDC.NotesAppFinal.Services.Interfaces;

    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services)
        {
            services.AddDbContext<NotesDbContext>(options => options.UseSqlServer(@"Data Source=(localdb)\LocalTest;Database=NotesAppFinalDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NotesRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<INotesService, NotesService>();
        }
    }
}
