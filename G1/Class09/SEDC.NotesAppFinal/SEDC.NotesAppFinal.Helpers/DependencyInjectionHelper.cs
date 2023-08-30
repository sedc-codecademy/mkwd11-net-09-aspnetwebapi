namespace SEDC.NotesAppFinal.Helpers
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using SEDC.NotesAppFinal.DataAccess;
    using SEDC.NotesAppFinal.DataAccess.AdoNetImplementation;
    using SEDC.NotesAppFinal.DataAccess.DapperImplementation;
    using SEDC.NotesAppFinal.DataAccess.Implementations;
    using SEDC.NotesAppFinal.DataAccess.Interfaces;
    using SEDC.NotesAppFinal.Domain.Models;
    using SEDC.NotesAppFinal.Services.Implementations;
    using SEDC.NotesAppFinal.Services.Interfaces;

    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NotesDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository<Note>, NotesRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<INotesService, NotesService>();
        }

        public static void InjectAdoNetRepository(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<AdoNetRepository>(x=> new AdoNetRepository(connectionString));
        }

        public static void InjectDapperRepository(this IServiceCollection services, string connectionString)
        {
            services.AddTransient<DapperRepository>(x => new DapperRepository(connectionString));
        }
    }
}
