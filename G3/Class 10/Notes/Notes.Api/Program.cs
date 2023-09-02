using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes.Data.Data;
using Notes.Data.Domain;
using Notes.Data.Repositories;
using Notes.Services.Mapping;
using Notes.Services.Models;
using Notes.Services.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddAutoMapper(typeof(NotesMap).Assembly);
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
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
