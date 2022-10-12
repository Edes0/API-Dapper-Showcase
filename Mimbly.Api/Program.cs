using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.Identity.Web;
using Mimbly.Application.Common.ServiceOptions;
using Mimbly.CoreServices.Configurations;
using Mimbly.CoreServices.Middlewares;
using Mimbly.Infrastructure.Identity.Context;
using Mimbly.Application;
using Mimbly.Application.Common.Mappings;
using Mimbly.Api.Extensions;
using Mimbly.Infrastructure.Security.Configurations;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
var services = builder.Services;

services.AddAutoMapper(typeof(MappingProfile).Assembly);
services.AddMediatR(typeof(ApplicationMediatREntrypoint).Assembly); //TODO: Göra snyggare?

// DataAccessLayer
services.ConfigureDataAccessManager();

// Repositories
services.ConfigureMimboxRepository();

// Services
services.ConfigureCors();
services.AddControllers();
services.ConfigureAppDbContext(builder.Configuration);
services.AddMediatR(Assembly.GetExecutingAssembly());
services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mimbly.Api", Version = "v1" }));

services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings")); //TODO: Ta bort?
services.Configure<MailGunConfig>(builder.Configuration.GetSection("MailGunConfig"));
services.Configure<MailGunConfig>(builder.Configuration.GetSection("MailGunConfig"));
services.Configure<FrontendApplicationConfig>(builder.Configuration.GetSection("FrontendApplicationConfig"));
services.AddSingleton(provider => provider.GetRequiredService<IOptions<ConnectionStrings>>().Value); //TODO: Ta bort?

// Authentication
// https://learn.microsoft.com/en-us/azure/active-directory/develop/scenario-protected-web-api-app-configuration
// https://learn.microsoft.com/en-us/azure/active-directory-b2c/enable-authentication-web-api?tabs=csharpclient

var azureAd = configurationBuilder.GetSection("AzureAd");
var azureAdConfig = azureAd.Get<AzureAdConfiguration>();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(azureAd);

Console.WriteLine(azureAdConfig.ClientId);

services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    var existingOnTokenValidatedHandler = options.Events.OnTokenValidated;
    options.Events.OnTokenValidated = async context =>
    {
        await existingOnTokenValidatedHandler(context);
        options.TokenValidationParameters.ValidAudiences = new[] { azureAdConfig.ClientId };
        options.TokenValidationParameters.ValidIssuer = azureAdConfig.Issuer;
    };
});

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
