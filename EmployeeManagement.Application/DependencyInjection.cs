using System.Reflection;
using Carter;
using EmployeeManagement.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Application;

//Introduce DI to application layer
public static class DependencyInjection
{
    /// <summary>
    /// Here we add the registration logic for the package libraries we added
    /// We add as extension method of IServiceCollection interface 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services) 
    {
        Assembly assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration => {
            configuration.RegisterServicesFromAssemblies(assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
        services.AddValidatorsFromAssembly(assembly);
        services.AddCarter();
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        //services.AddSingleton<ICafeRepository, CafeRe>();

        services;
}

