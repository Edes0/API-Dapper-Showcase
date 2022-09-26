namespace Boilerplate.Application;

using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class MapperDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}