using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using RouteFinderAPI.Business.MappingProfile;
using RouteFinderAPI.Data.Contexts;
using RouteFinderAPI.Data.Interfaces;
using RouteFinderAPI.Models.ViewModels;
using RouteFinderAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRouteFinderDatabase, RouteFinderContext>(_ => new RouteFinderContext("Server=db,5432;Database=routefinder;User Id=postgres;Password=password;"));
builder.Services.AddFluentValidation(s => s.RegisterValidatorsFromAssemblyContaining<UserCreateViewValidator>());
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IPlotpointService, PlotpointService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(
    typeof(PlotpointMappingProfile),
    typeof(RouteMappingProfile),
    typeof(TypeMappingProfile),
    typeof(UserMappingProfile)
);

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