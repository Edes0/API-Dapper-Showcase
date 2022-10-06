using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mimbly.Application.Common.Interfaces.ExternalServices.MailServices;
using Mimbly.Application.Common.Interfaces;
using Mimbly.Application.Common.ServiceOptions;
using Mimbly.CoreServices.Configurations;
using Mimbly.CoreServices.Middlewares;
using Mimbly.Infrastructure.ExternalServices.Interfaces.ExternalServices.MailServices;
using Mimbly.Infrastructure.ExternalServices.MailServices;
using Mimbly.Infrastructure.Identity.Context;
using Mimbly.Infrastructure.Security.Configurations;
using Mimbly.Infrastructure.Security.Tokens;
using Mimbly.Infrastructure.Security.Tokens.Interfaces;
using Mimbly.Persistence.Repositories;
using Mimbly.Infrastructure.Security.Tokens;
using Mimbly.Application;
using Mimbly.Application.Common.Mappings;
using Mimbly.Api.Extensions;

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
services.ConfigureIdentityRepository();

// Services
services.ConfigureMailService();
services.ConfigureMailService();
services.ConfigureInviteUserService();
services.ConfigureResetPasswordService();
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

var tokenConfig = configurationBuilder.GetSection("TokenConfig");

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = tokenConfig.Get<TokenConfig>().Issuer,
        ValidAudience = tokenConfig.Get<TokenConfig>().Audience,
        IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig
                    .Get<TokenConfig>()
                    .SecretKey))
    });

// Add secret key to signin configurations.
var signingConfigurations = new SigningConfigurations(tokenConfig
    .Get<TokenConfig>()
    .SecretKey);
services.AddSingleton(signingConfigurations);

services.Configure<TokenConfig>(tokenConfig);
services.AddScoped<ITokenHandler, Mimbly.Infrastructure.Security.Tokens.TokenHandler>();

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

app.UseCors("WhatToAdd"); //TODO Add specific origin to cors. also have in config in ServiceExtensions "CorsPolicy"
app.UseHttpsRedirection();
app.UseMiddleware(typeof(ExceptionMiddleware));
app.Use(async (context, next) => await ControllerExceptionMiddleware.HandleControllerExceptions(context, next));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
