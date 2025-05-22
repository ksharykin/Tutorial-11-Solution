using AnonPosters.API.DAL;
using Microsoft.EntityFrameworkCore;

// To perform code-first apporach we need to execute next commands:
// dotnet ef migrations add InitialAnonPostersDB
// It prepares migrations to database
// To finish migrations, we need to execute
// dotnet ef database update

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PosterDB") ?? throw new Exception("Connection string not found");
// Add services to the container.
builder.Services.AddDbContext<AnonPostersContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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