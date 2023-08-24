using Microsoft.Extensions.DependencyInjection;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class RegisterDependencies
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ITestService, TestServiceUpgraded>();
        }
    }
}
