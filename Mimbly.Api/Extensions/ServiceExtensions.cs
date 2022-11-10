namespace Mimbly.Api.Extensions;

using MediatR;
using PuppeteerSharp;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Mimbly.Application;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Common.Mappings;
using Mimbly.CoreServices.Logger;
using Mimbly.CoreServices.PuppeteerServices;
using Mimbly.Infrastructure.Identity.Context;
using Mimbly.Persistence.Repositories;
using NLog;

public static class PuppeteerExtensions
{
    private static string _executablePath;
    public static async Task PreparePuppeteerAsync(this IServiceCollection service,
        IWebHostEnvironment hostingEnvironment)
    {
        // Downloads & Installs a chromium browser.
        var downloadPath = Path.Join(hostingEnvironment.ContentRootPath, "./puppeteer");
        var browserOptions = new BrowserFetcherOptions { Path = downloadPath };
        var browserFetcher = new BrowserFetcher(browserOptions);
        _executablePath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultChromiumRevision);
        await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
    }

    public static string ExecutablePath => _executablePath;
}


public static class ServiceExtensions
{
    public static void ConfigureDataAccessManager(this IServiceCollection services) =>
          services.AddScoped<ISqlDataAccess, SqlDataAccess>();

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMimboxRepository, MimboxRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyContactRepository, CompanyContactRepository>();
    }

    public static void ConfigureCors(this IServiceCollection services, string allowedOrigins) =>
    services.AddCors(opts => opts.AddPolicy(allowedOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://mimbly-frontend.azurewebsites.net/"); // TODO: TA BORT LOCALHOST VID SKARP RELEASE
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    }));

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
        Console.WriteLine("AZUREAD OBJECT SHOULD BE HERE ->>>" + azureAd.ToString());

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

    public static void ConfigurePuppeteer(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddControllersWithViews();
        services.AddScoped<ITemplateService, ViewTemplateService>();
        services.PreparePuppeteerAsync(environment).GetAwaiter().GetResult();
        services.Configure<RazorViewEngineOptions>(opt =>
        {
            opt.ViewLocationExpanders.Add(new ViewLocationExpander());
            opt.ViewLocationFormats.Add("/DocumentTemplates/{0}.cshtml");
        });
    }
}