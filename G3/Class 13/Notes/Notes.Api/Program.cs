using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Notes.BCryptPasswordHasher;
using Notes.Data.Data;
using Notes.Data.Domain;
using Notes.Data.Repositories;
using Notes.Services;
using Notes.Services.Mapping;
using Notes.Services.Models;
using Notes.Services.Service;
using Notes.Services.Service.External;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging();
builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});
var connectionString = builder.Configuration.GetConnectionString("NotesConnection");
builder.Services.AddSqlServer<NotesDbContext>(
    connectionString, 
    sqlOption =>
    {
    },
    dbContextOptions =>
    {
        dbContextOptions.UseLazyLoadingProxies();
    });
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });
});
builder.Services.AddScoped<INotesRepository, NotesRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddAutoMapper(typeof(NotesMap).Assembly);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    var secret = builder.Configuration["SecretKey"];
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
})
;

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("Admin").RequireClaim(ClaimTypes.Country);
    });
});

builder.Services.AddHttpClient(HttpClients.Profiles, httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["ProfilesApi"]);
});

builder.Services.AddHttpClient(HttpClients.PlaceholderApi, httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["PlaceholderApi"]);
});
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IPlaceholderService, PlaceholderService>();
//var cfg = new MapperConfiguration(cg =>
//{
//    cg.CreateMap<Note, NoteModel>()
//                .ForMember(x => x.Tags,
//                m => m.MapFrom(x => x.Tags.Select(x => x.Name)));
//});
//builder.Services.AddScoped((sp) => cfg.CreateMapper());
//builder.Services.AddDbContext<NotesDbContext>(opt =>
//{
//    opt.UseSqlServer(connectionString);
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(opt =>
    {
    });
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
