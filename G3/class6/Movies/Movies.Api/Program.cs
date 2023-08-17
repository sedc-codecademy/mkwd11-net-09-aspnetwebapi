using Microsoft.EntityFrameworkCore;
using Movies.Api.Middlewares;
using Movies.BLL.Services;
using Movies.DAL.Context;
using Movies.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSqlServer<MoviesDbContext>(connectionString);

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<ExceptionHandlingMiddlware>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    MoviesDbContext context = scope.ServiceProvider.GetRequiredService<MoviesDbContext>();
    context.Database.Migrate();
}

app.UseMiddleware<ExceptionHandlingMiddlware>();
app.UseMiddleware<LoggingMiddlware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
