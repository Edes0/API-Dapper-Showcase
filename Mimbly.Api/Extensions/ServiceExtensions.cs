namespace Mimbly.Api.Extensions;

using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Mimbly.Application;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Common.Mappings;
using Mimbly.CoreServices.Logger;
using Mimbly.Infrastructure.Identity.Context;
using Mimbly.Persistence.Repositories;
using NLog;

public static class ServiceExtensions
{
    public static void ConfigureDataAccessManager(this IServiceCollection services) =>
          services.AddScoped<ISqlDataAccess, SqlDataAccess>();

    public static void ConfigureRepositories(this IServiceCollection services) =>
          services.AddScoped<IMimboxRepository, MimboxRepository>();

    public static void ConfigureCors(this IServiceCollection services) => //TODO: Add allowed SpecificOrigins?
    services.AddCors(opts => opts.AddPolicy("CorsPolicy", builder =>
               builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowAnyOrigin()));

    public static void ConfigureAppDbContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DbConnectionString"), b => b.MigrationsAssembly("Mimbly.Api")));

    public static void ConfigureLogger(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();

        LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
    }

    public static void ConfigureNugetPackages(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddMediatR(typeof(ApplicationMediatREntrypoint).Assembly);
        services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mimbly.Api", Version = "v1" }));
    }

    public static void ConfigureAuthentication(this IServiceCollection services, IConfigurationRoot configurationBuilder)
    {

        var azureAd = configurationBuilder.GetSection("AzureAd");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(azureAd);

        // Optional code for multible AD's. //TODO: Maybe move this?

        // var azureAdConfig = azureAd.Get<AzureAdConfiguration>();

        //services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
        //{
        //    var existingOnTokenValidatedHandler = options.Events.OnTokenValidated;
        //    options.Events.OnTokenValidated = async context =>
        //    {
        //        await existingOnTokenValidatedHandler(context);
        //        options.TokenValidationParameters.ValidAudiences = new[] { azureAdConfig.ClientId };
        //        options.TokenValidationParameters.ValidIssuer = azureAdConfig.Issuer;
        //    };
        //});

        // https://learn.microsoft.com/en-us/azure/active-directory/develop/scenario-protected-web-api-app-configuration
        // https://learn.microsoft.com/en-us/azure/active-directory-b2c/enable-authentication-web-api?tabs=csharpclient
    }
}