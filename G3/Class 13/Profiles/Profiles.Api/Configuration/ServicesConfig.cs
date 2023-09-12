﻿using Profiles.BLL.Services;
using Profiles.DAL.Repositories;

namespace Profiles.Api.Configuration
{
    public static class ServicesConfig
    {
        public static IServiceCollection ConfigServices(this IServiceCollection services)
        {
            return services.AddScoped<IProfileService, ProfileService>()
                .AddScoped<IProfileRepository, ProfileRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddSingleton<IHasher, BCryptHasher>()
                .AddSingleton<IEmailSender, EmailSender>()
                .AddSingleton<ISignInManager, JWTSigninManger>();
        }
    }
}
