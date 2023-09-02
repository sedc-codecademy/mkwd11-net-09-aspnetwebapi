using Profiles.DAL.Data;

namespace Profiles.Api.Configuration
{
    public static class DbContextConfig
    {
        public static IServiceCollection ConfigDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            return services.AddSqlServer<ProfileDbContext>(connectionString);
        }
    }
}
