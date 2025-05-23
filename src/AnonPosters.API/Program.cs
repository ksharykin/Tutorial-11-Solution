using AnonPosters.API.DAL;
using AnonPosters.API.Helpers.Extensions;
using AnonPosters.API.Helpers.Options;
using AnonPosters.API.Services.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// To perform code-first apporach we need to execute next commands:
// dotnet ef migrations add InitialAnonPostersDB
// It prepares migrations to database
// To finish migrations, we need to execute
// dotnet ef database update

var builder = WebApplication.CreateBuilder(args);
//TODO: Check if read correctly
var jwtConfigData = builder.Configuration.GetSection(JwtOptions.ConfigKey);

var connectionString = builder.Configuration.GetConnectionString("PosterDB") ?? throw new Exception("Connection string not found");
// Add services to the container.
builder.Services.AddDbContext<AnonPostersContext>(options => options.UseSqlServer(connectionString));
builder.Services.Configure<JwtOptions>(jwtConfigData);
builder.Services.AddJwt(jwtConfigData.Get<JwtOptions>() ?? throw new Exception("JWT configuration not found"));
builder.Services.AddAuthorization();
builder.Services.AddScoped<ITokenService, TokenService>();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();