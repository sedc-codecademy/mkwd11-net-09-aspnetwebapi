
using Microsoft.EntityFrameworkCore;
using SEDC.NotesAppFluentApi.DataAccess;
using SEDC.NotesAppFluentApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add conection string directlry in Program.cs
//builder.Services.AddDbContext<NotesAppDbContext>(options =>
//{
//    options.UseSqlServer("server=./sqlexpress;database=noteappdbfluent;trusted_connection=true;Encrypt=False");
//});

//if you are using the default naming of ConnectionStrings
var connectionString = builder.Configuration.GetConnectionString("Default");

//if you are not using the default naming of ConnectionStrings
//var connectionString2 = builder.Configuration["ConnectionStrings:Default"];  

//DependencyInjectionHelper.InjectDbContext(builder.Services, connectionString);

DependencyInjectionHelper.InjectDbContext(builder.Services, builder.Configuration);

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
