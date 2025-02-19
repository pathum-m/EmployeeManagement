using System.Reflection;
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

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(assembly));
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}

