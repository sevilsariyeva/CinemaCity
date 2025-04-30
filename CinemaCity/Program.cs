using System.Text.Json.Serialization;
using CinemaCity.Data;
using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using CinemaCity.Repositories.Concrete;
using CinemaCity.Services.Abstract;
using CinemaCity.Services.Concrete;
using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

Env.Load();

var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IFilmRepository, FilmRepository>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();


var connection = builder.Configuration.GetConnectionString("DefaultConnection");
var frontendUrl = configuration.GetValue<string>("frontend_url");
var adminUrl = configuration.GetValue<string>("admin_url");
builder.Services.AddDbContext<CinemaCityDbContext>(options =>
{
    options.UseSqlServer(connection);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.WithOrigins(frontendUrl, adminUrl)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
});
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;
    });
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<CinemaCityDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();
public partial class Program { }
