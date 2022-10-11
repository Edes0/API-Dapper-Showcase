namespace Mimbly.Api.Extensions;

using Microsoft.EntityFrameworkCore;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Infrastructure.Identity.Context;
using Mimbly.Persistence.Repositories;

public static class ServiceExtensions
{
    public static void ConfigureDataAccessManager(this IServiceCollection services) =>
          services.AddScoped<ISqlDataAccess, SqlDataAccess>();

    public static void ConfigureMimboxRepository(this IServiceCollection services) =>
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
}