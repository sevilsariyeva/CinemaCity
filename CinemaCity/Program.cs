using CinemaCity.Data;
using CinemaCity.Models;
using CinemaCity.Repositories.Abstract;
using CinemaCity.Repositories.Concrete;
using CinemaCity.Services.Abstract;
using CinemaCity.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddScoped<IUserRepository, UserRepository>();


var connection = builder.Configuration.GetConnectionString("DefaultConnection");
var frontendUrl = configuration.GetValue<string>("frontend_url");
var adminUrl = configuration.GetValue<string>("admin_url");
builder.Services.AddDbContext<AppDBContext>(options =>
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
