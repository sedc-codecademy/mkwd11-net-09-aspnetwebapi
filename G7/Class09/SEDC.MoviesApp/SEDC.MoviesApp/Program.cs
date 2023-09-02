using Microsoft.EntityFrameworkCore;
using SEDC.MoviesApp.DataAccess;
using SEDC.MoviesApp.Domain;
using SEDC.MoviesApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MovieAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDbConnectionString"));
});

builder.Services.AddTransient<IRepository<Movie>, Repository<Movie>>();
//builder.Services.AddTransient<IRepository<Note>, Repository<Note>>();
builder.Services.AddTransient<IMovieService, MovieService>();

var app = builder.Build();

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
