using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.DataAnnotations.DataAccess;

namespace SEDC.DataAnnotations.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(this IServiceCollection services)
        {
            services.AddDbContext<NotesAppDbContext>(options => options.UseSqlServer(@"Data Source=(localdb)\LocalTest;Database=NotesAppDADb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
        }
    }
}
