using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mimbly.Api.DependencyInjection;
using Mimbly.CoreServices.Middlewares;
using Mimbly.Infrastructure.Identity.Context;
using Mimbly.Infrastructure.Security.Configurations;
using Mimbly.Infrastructure.Security.Tokens;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
var services = builder.Services;

const string allowedSpecificOrigins = "AllowedSpecificOrigins";

services.AddControllers();
services.AddMediatR(Assembly.GetExecutingAssembly());

services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mimbly.Api", Version = "v1" }));

// When copying the project make sure to change name of the migration assembly to the correct name.
services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString"), b => b.MigrationsAssembly("Mimbly.Api")));
;

// Specify specific cors options here later.
services.AddCors(options => options.AddPolicy(allowedSpecificOrigins, policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

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

// Dependency injection
DependencyInjection.AddDependencyInjection(services, builder, tokenConfig);

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

app.UseCors(allowedSpecificOrigins);
app.UseHttpsRedirection();
app.UseMiddleware(typeof(ExceptionMiddleware));
app.Use(async (context, next) => await ControllerExceptionMiddleware.HandleControllerExceptions(context, next));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
