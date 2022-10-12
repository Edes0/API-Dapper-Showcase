using Mimbly.Api.Extensions;
using Mimbly.CoreServices.Middlewares;
using Mimbly.Infrastructure.Identity.Context;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
var services = builder.Services;

// DataAccessLayer
services.ConfigureDataAccessManager();

// Repositories
services.ConfigureRepositories();

// Services
services.ConfigureCors();
services.AddControllers();
services.ConfigureAppDbContext(builder.Configuration);
services.ConfigureNugetPackages();

// Authentication
services.ConfigureAuthentication(builder.Configuration);

// Build
var app = builder.Build();

// This ensure that the database is created with seed when in development mode if it does not already exist.
// Additional environments can be added here later therefore it is separated from the line below.
if (app.Environment.IsDevelopment())
{
    using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
    var context = serviceScope?.ServiceProvider.GetRequiredService<AppDbContext>();
    context?.Database.EnsureCreated();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mimbly.Api v1"));
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.UseCors("WhatToAdd"); //TODO Add specific origin to cors. also have in config in ServiceExtensions "CorsPolicy"
app.UseMiddleware(typeof(ExceptionMiddleware));
app.Use(async (context, next) => await ControllerExceptionMiddleware.HandleControllerExceptions(context, next));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
