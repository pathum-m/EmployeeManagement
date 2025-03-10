using EmployeeManagement.Application.Abstractions;
using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Infrastructure.Repositories;
using EmployeeManagement.Infrastructure.Services;
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
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICafeRepository, CafeRepository>();
        services.AddScoped<IEmployeeRespository, EmployeeRepository>();
        services.AddScoped<IImageService, ImageService>();
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

