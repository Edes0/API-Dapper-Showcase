using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mimbly.Api.Extensions;
using Mimbly.CoreServices.Middlewares;
using Mimbly.Infrastructure.Identity.Context;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables()
    .Build();

const string AllowedOrigins = "_allowedOrigins";

// Add services to the container.
var services = builder.Services;

// DataAccessLayer
services.ConfigureDataAccessManager();

// Repositories
services.ConfigureRepositories();

// Services
services.ConfigureCors(AllowedOrigins, builder.Configuration);
services.AddControllers();
services.ConfigureAppDbContext(builder.Configuration);
services.ConfigureNugetPackages();

// Puppeteer
services.ConfigurePuppeteer(builder.Environment);

// Versioning
services.ConfigureVersioning();

// Authentication & Authorization
services.ConfigureAuthentication(builder.Configuration);
services.ConfigureAuthAttribute();

// Account Service
services.ConfigureAccountService();

// Build
var app = builder.Build();

// This ensure that the database is created with seed when in development mode if it does not already exist.
// Additional environments can be added here later therefore it is separated from the line below.
if (app.Environment.IsDevelopment())
{
    using var serviceScope = app.Services.GetService<IServiceScopeFactory>()?.CreateScope();
    var context = serviceScope?.ServiceProvider.GetRequiredService<AppDbContext>();
    context?.Database.EnsureCreated();

    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
        {
            opt.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
    app.UseHsts();
}

app.UseCors(AllowedOrigins);
app.UseMiddleware(typeof(ExceptionMiddleware));
app.Use(async (context, next) => await ControllerExceptionMiddleware.HandleControllerExceptions(context, next));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
