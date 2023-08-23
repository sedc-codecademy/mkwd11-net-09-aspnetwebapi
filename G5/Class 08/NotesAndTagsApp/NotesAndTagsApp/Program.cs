using NotesAndTagsApp.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DependencyInjectionHelper.InjectDbContext(builder.Services);

DependencyInjectionHelper.InjectRepositories(builder.Services);
DependencyInjectionHelper.InjectServices(builder.Services);

//read from appSettings.json, find the property DbSettings
var appSettings = builder.Configuration.GetSection("DbSettings");
builder.Services.Configure<DatabaseSettings>(appSettings); //map the appSettings into the class DatabaseSettings
DatabaseSettings appSettingsObject = appSettings.Get<DatabaseSettings>(); //create an object with values from app settings

DependencyInjectionHelper.InjectAdoRepositories(builder.Services, appSettingsObject.ConnectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
