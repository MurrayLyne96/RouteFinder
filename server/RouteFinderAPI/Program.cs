using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RouteFinderAPI.Authentication;
using RouteFinderAPI.Business.MappingProfile;
using RouteFinderAPI.Data.Contexts;
using RouteFinderAPI.Data.Interfaces;
using RouteFinderAPI.Middleware;
using RouteFinderAPI.Models.ViewModels;
using RouteFinderAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddAuthentication(string.Empty).AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>(string.Empty, options => {});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(string.Empty, policy =>
    {
        policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddScoped<IRouteFinderDatabase, RouteFinderContext>(_ => new RouteFinderContext(EnvironmentVariables.DbConnectionString));
builder.Services.AddFluentValidation(s => s.RegisterValidatorsFromAssemblyContaining<UserCreateViewValidator>());
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IPlotpointService, PlotpointService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddHealthChecks();

builder.Services.AddAutoMapper(config => config.AllowNullCollections = true, typeof(Program).Assembly,
    typeof(RouteService).Assembly);

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

app.UseCors(
    o => o
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
);

app.MapControllers().RequireAuthorization();

app.MapHealthChecks("/health");

app.Run();

public partial class Program { };