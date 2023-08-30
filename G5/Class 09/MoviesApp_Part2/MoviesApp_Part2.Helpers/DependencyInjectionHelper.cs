using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp_Part2.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp_Part2.Helpers
{
    public static class DependencyInjectionHelper
    {

        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MoviesAppDbContext>(x => x.UseSqlServer(connectionString));
          //  services.AddDbContext<MoviesAppDbContext>(x => x.UseSqlServer("Server =.; Database=MoviesAppDbG5; Trusted_Connection = True; TrustServerCertificate = True;"));
        }
    }
}
