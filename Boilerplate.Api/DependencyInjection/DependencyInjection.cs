namespace Boilerplate.Api.DependencyInjection;

using Application.Common.Interfaces.ExternalServices.MailServices;
using Boilerplate.Application;
using Boilerplate.Application.Common.Interfaces;
using Boilerplate.Application.Common.ServiceOptions;
using Boilerplate.Infrastructure.Security.Tokens;
using Boilerplate.Infrastructure.Security.Tokens.Interfaces;
using Boilerplate.Persistence.Repositories;
using CoreServices.Configurations;
using Infrastructure.ExternalServices.Interfaces.ExternalServices.MailServices;
using Infrastructure.ExternalServices.MailServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

public static class DependencyInjection
{
    public static void AddDependencyInjection(IServiceCollection services, WebApplicationBuilder builder, IConfigurationSection tokenConfig)
    {
        // Repositories
        services.AddScoped<IBoilerplateRepository, BoilerplateRepository>();
        services.AddScoped<IIdentityRepository, IdentityRepository>();

        // Services
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IInviteUserService, InviteUserService>();
        services.AddScoped<IResetPasswordService, ResetPasswordService>();

        services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
        services.Configure<MailGunConfig>(builder.Configuration.GetSection("MailGunConfig"));
        services.Configure<MailGunConfig>(builder.Configuration.GetSection("MailGunConfig"));
        services.Configure<FrontendApplicationConfig>(builder.Configuration.GetSection("FrontendApplicationConfig"));
        services.AddSingleton(provider => provider.GetRequiredService<IOptions<ConnectionStrings>>().Value);
        services.Configure<TokenConfig>(tokenConfig);

        // Initiate
        services.AddApplication();
    }
}