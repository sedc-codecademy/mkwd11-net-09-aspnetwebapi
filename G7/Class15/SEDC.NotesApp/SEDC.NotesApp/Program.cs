using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SEDC.NotesApp.Helpers;
using Serilog;
using Serilog.Events;
using System.Text;

namespace SEDC.NotesApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
                                            
                    });
            });

            DependencyInjectionHelper.InjectDbContext(builder.Services, builder.Configuration);
            DependencyInjectionHelper.InjectRepositories(builder.Services);
            DependencyInjectionHelper.InjectServices(builder.Services);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                //caches the token so that we can access it during the request lifetime
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    //Check if the expiration time has passes
                    ValidateLifetime = true,
                    //Token musth have an expiration time
                    RequireExpirationTime = true,
                    //should be sto to true to validate the key
                    ValidateIssuerSigningKey = true,
                    // set the time buffer for client-server difference. default is 300 sec
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:SecretKey"]))
                };
            });

            //This overides the manual setting we do below
            builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(builder.Configuration));

            //When setting the configuration from Program.cs
            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.Console()
            //    //.WriteTo.File("logs/log.txt")
            //    .CreateLogger();

            //We use the appsetings configuration
            //Log.Logger = new LoggerConfiguration().
            //    ReadFrom.Configuration(builder.Configuration)
            //    .CreateLogger();

           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}