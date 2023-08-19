using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.NotesAppDA.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.NotesAppDA.Helpers
{
    public class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<NotesAppDataAnotationDBContext>(x => x.UseSqlServer("Server=.\\DANILO;Database=NotesDataAnotationDb;Trusted_Connection=True;TrustServerCertificate=True"));
        }
    }
}
