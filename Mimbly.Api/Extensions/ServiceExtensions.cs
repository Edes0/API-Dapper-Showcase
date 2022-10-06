namespace Mimbly.Api.Extensions;

using Microsoft.EntityFrameworkCore;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Common.Interfaces.ExternalServices.MailServices;
using Mimbly.Infrastructure.ExternalServices.Interfaces.ExternalServices.MailServices;
using Mimbly.Infrastructure.ExternalServices.MailServices;
using Mimbly.Infrastructure.Identity.Context;
using Mimbly.Persistence.Repositories;

public static class ServiceExtensions
{
    public static void ConfigureDataAccessManager(this IServiceCollection services) =>
          services.AddScoped<ISqlDataAccess, SqlDataAccess>();

    public static void ConfigureMimboxRepository(this IServiceCollection services) =>
          services.AddScoped<IMimboxRepository, MimboxRepository>();

    public static void ConfigureIdentityRepository(this IServiceCollection services) =>
          services.AddScoped<IIdentityRepository, IdentityRepository>();

    public static void ConfigureMailService(this IServiceCollection services) =>
          services.AddScoped<IMailService, MailService>();

    public static void ConfigureInviteUserService(this IServiceCollection services) =>
          services.AddScoped<IInviteUserService, InviteUserService>();

    public static void ConfigureResetPasswordService(this IServiceCollection services) =>
          services.AddScoped<IResetPasswordService, ResetPasswordService>();

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
}