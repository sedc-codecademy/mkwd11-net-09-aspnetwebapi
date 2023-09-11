using DataAccess;
using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace Utilities
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection RegisterModule(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NotesAppDbContext>(x =>
                x.UseSqlServer(connectionString));
            services.AddTransient<IRepository<NoteDto>, NoteDapperRepository>();
            services.AddTransient<IRepository<UserDto>, UserRepository>();
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}
