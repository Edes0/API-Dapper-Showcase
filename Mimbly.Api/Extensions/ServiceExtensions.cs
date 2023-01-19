namespace Mimbly.Api.Extensions;

using Attributes;
using Business.Helpers.AD;
using Business.Interfaces.AD;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Mimbly.Application;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Common.Mappings;
using Mimbly.CoreServices.Authorization;
using Mimbly.CoreServices.PuppeteerServices;
using Mimbly.Infrastructure.Identity.Context;
using Mimbly.Persistence.Repositories;
using PuppeteerSharp;

public static class PuppeteerExtensions
{
    /// <summary>
    /// Downloads and installs a chromium browser locally, to be
    /// used headless for pdfs.
    /// </summary>
    /// <param name="service"></param>
    /// <param name="hostingEnvironment"></param>
    public static async Task PreparePuppeteerAsync(this IServiceCollection service,
        IWebHostEnvironment hostingEnvironment)
    {
        // Downloads & Installs a chromium browser.
        var downloadPath = Path.Join(System.IO.Directory.GetCurrentDirectory(), "./puppeteer");
        var browserOptions = new BrowserFetcherOptions { Path = downloadPath };
        var browserFetcher = new BrowserFetcher(browserOptions);
        ExecutablePath = browserFetcher.GetExecutablePath(BrowserFetcher.DefaultChromiumRevision);
        await browserFetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
    }

    public static string? ExecutablePath { get; private set; }
}

public static class ServiceExtensions
{
    /// <summary>
    /// Configures the data access layer that
    /// is used by data persistence services.
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureDataAccessManager(this IServiceCollection services) =>
          services.AddScoped<ISqlDataAccess, SqlDataAccess>();

    /// <summary>
    /// Configures services for data persistence for
    /// multiple different entities.
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMimboxRepository, MimboxRepository>();
        services.AddScoped<IMimboxStatusRepository, MimboxStatusRepository>();
        services.AddScoped<IMimboxModelRepository, MimboxModelRepository>();
        services.AddScoped<IMimboxLocationRepository, MimboxLocationRepository>();
        services.AddScoped<IMimboxErrorLogRepository, MimboxErrorLogRepository>();
        services.AddScoped<IMimboxContactRepository, MimboxContactRepository>();
        services.AddScoped<IMimboxLogRepository, MimboxLogRepository>();
        services.AddScoped<IMimboxLogImageRepository, MimboxLogImageRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<ICompanyContactRepository, CompanyContactRepository>();
        services.AddScoped<IEventLogRepository, EventLogRepository>();
    }

    /// <summary>
    /// Configures cors according to the environment variable "corsUrl".
    /// </summary>
    /// <param name="services"></param>
    /// <param name="allowedOrigins"></param>
    /// <param name="config"></param>
    public static void ConfigureCors(this IServiceCollection services, string allowedOrigins, IConfiguration config) =>
    services.AddCors(opts => opts.AddPolicy(allowedOrigins, policy =>
    {
        var corsUrl = config.GetValue<string>("CorsUrl");
        policy.WithOrigins(corsUrl);
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    }));

    /// <summary>
    /// Configures the SQL database for the application.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureAppDbContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("DbConnectionString"), b => b.MigrationsAssembly("Mimbly.Api")));

    /// <summary>
    /// Configures AutoMapper and MediatR.
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureNugetPackages(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        services.AddMediatR(typeof(ApplicationMediatREntrypoint).Assembly);
    }

    /// <summary>
    /// Configures authentication with Azure Ad.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configurationBuilder"></param>
    public static void ConfigureAuthentication(this IServiceCollection services, IConfigurationRoot configurationBuilder)
    {
        var azureAd = configurationBuilder.GetSection("AzureAd");
        Console.WriteLine("AZUREAD OBJECT SHOULD BE HERE ->>>" + azureAd.ToString());

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApi(azureAd);
    }

    /// <summary>
    /// Configures puppeteer that is used to create
    /// pdf documents using a headless chrome instance.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="environment"></param>
    public static void ConfigurePuppeteer(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddControllersWithViews(opt => opt.Filters.Add(typeof(EasyValidationAttribute)));
        services.AddScoped<ITemplateService, ViewTemplateService>();
        services.PreparePuppeteerAsync(environment).GetAwaiter().GetResult();
        services.Configure<RazorViewEngineOptions>(opt =>
        {
            opt.ViewLocationExpanders.Add(new ViewLocationExpander());
            opt.ViewLocationFormats.Add("/DocumentTemplates/{0}.cshtml");
        });
    }

    /// <summary>
    /// Configures versioning for the controllers
    /// and also enables it in Swagger.
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new MediaTypeApiVersionReader("x-api-version"));
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureOptions<SwaggerExtension>();
    }

    /// <summary>
    /// Configures role based authorization attribute.
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureAuthAttribute(this IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, GroupsPolicyProvider>();
        services.AddSingleton<IAuthorizationHandler, GroupsHandler>();
    }

    /// <summary>
    /// Configures services and helpers to handle
    /// Azure Ad entities, such as users and groups.
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureAccountService(this IServiceCollection services)
    {
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IGraphService, GraphService>();
        services.AddSingleton<IGraphHelper, GraphHelper>();
    }

    /// <summary>
    /// Configures custom validation responses according to
    /// the requirements from the frontend.
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureCustomValidationResponse(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }
}