using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NotesAppFluentApi.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesAppFluentApi.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services) 
        {
            services.AddDbContext<NotesAppDbContext>(options =>
            {
                options.UseSqlServer("server=./sqlexpress;database=noteappdbfluent;trusted_connection=true;Encrypt=False");
            });

        }

        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<NotesAppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }

        public static void InjectDbContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotesAppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
        }
    }
}
