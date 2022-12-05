using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using RouteFinderAPI.Data.Contexts;
using RouteFinderAPI.Data.Interfaces;
using RouteFinderAPI.Models.ViewModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRouteFinderDatabase, RouteFinderContext>(_ => new RouteFinderContext("Server=localhost,5432;Database=routefinder;User Id=postgres;Password=password;"));
builder.Services.AddFluentValidation(s => s.RegisterValidatorsFromAssemblyContaining<UserCreateViewValidator>());
builder.Services.AddFluentValidationAutoValidation();
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