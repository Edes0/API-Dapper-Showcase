namespace Mimbly.Application;

using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class MapperDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) //TODO: FLYTTA ALLA DEPENDENCYINJECTIONS TILL PROGRAM.CS
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());//TODO: Ändra Assembly till Mimbly.Application.Assemly

        return services;
    }
}