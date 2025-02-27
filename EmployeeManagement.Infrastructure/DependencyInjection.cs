using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) => services
        .AddServices()
        .AddDatabase(configuration);

    private static IServiceCollection AddServices(this IServiceCollection services) 
    {
        services.AddScoped<ICafeRepository, CafeRepository>();
        services.AddScoped<IEmployeeRespository, EmployeeRepository>();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<EmployeeDBContext>(
            options => options
                .UseSqlServer(connectionString));

        return services;
    }
}

