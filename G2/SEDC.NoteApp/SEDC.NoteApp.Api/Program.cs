//nuget packages
//Microsoft.EntityFrameworkCore.Design

//EF commands
// 1. add-migration [migration_name] (example: initial)
// 2. update-database

using SEDC.NoteApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");

// without extension method
//DependencyInjectionHelper.InjectDbContext(builder.Services, cs);

//with extension method
builder.Services.InjectDbContext(cs);
builder.Services.RegisterRepositories();

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
