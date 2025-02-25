using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) => services
        .AddServices();

    private static IServiceCollection AddServices(this IServiceCollection services) 
    {
        services.AddScoped<ICafeRepository, CafeRepository>();
        services.AddScoped<IEmployeeRespository, EmployeeRepository>();
        return services;
    }    
}

