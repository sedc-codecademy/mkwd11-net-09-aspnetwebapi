using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp_Part2.DataAccess;
using MoviesApp_Part2.DataAccess.Implementations;
using MoviesApp_Part2.DataAccess.Interfaces;
using MoviesApp_Part2.Domain.Models;
using MoviesApp_Part2.Services.Implementations;
using MoviesApp_Part2.Services.Interfaces;
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

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IMovieService, MovieService>();
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IMovieRepository, MovieRepository>();
        }
    }
}
