//nuget packages
//Microsoft.EntityFrameworkCore.Design
//Microsoft.AspNetCore.Authentication.JwtBearer

//EF commands
// 1. add-migration [migration_name] (example: initial)
// 2. update-database

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SEDC.NoteApp.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");

//Ways to get values from AppSettings (OLD WAY)
var sectionValue = builder.Configuration.GetSection("RandomSection").Value;
var secretKeyFromAppSettings = builder.Configuration.GetSection("NoteAppSettings").GetValue<string>("SecretKey");

//Ways to get values from AppSettings (NEW WAY)
var noteAppSettings = builder.Configuration.GetSection("NoteAppSettings");
var noteAppSettingsObject = noteAppSettings.Get<NoteAppSettings>();

// DEPENDENCY INJECTION
// without extension method
//DependencyInjectionHelper.InjectDbContext(builder.Services, cs);

//with extension method
builder.Services.InjectDbContext(cs);
builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

var secretKey = Encoding.ASCII.GetBytes(noteAppSettingsObject.SecretKey);

// CONFIGURE JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
