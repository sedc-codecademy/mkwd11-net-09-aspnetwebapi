using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NoteApp.DataAccess;
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
        public static void InjectDbContext(IServiceCollection services)
        {
            services.AddDbContext<NoteAppDbContext>(options => 
                options.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=NoteAppDb;Trusted_Connection=True"));
        }
    }
}
