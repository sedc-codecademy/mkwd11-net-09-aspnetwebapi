using MoviesApp_Part2.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//read connection string from appSettings.json
var appSettings = builder.Configuration.GetSection("DbSettings");
builder.Services.Configure<DbSettings>(appSettings);

DbSettings dbSettingsObject = appSettings.Get<DbSettings>();

//Inject db context
DependencyInjectionHelper.InjectDbContext(builder.Services, dbSettingsObject.ConnectionString);

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
