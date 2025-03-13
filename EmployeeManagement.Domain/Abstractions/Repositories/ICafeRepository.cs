using System.Linq.Expressions;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;

namespace EmployeeManagement.Domain.Abstractions.Repositories;

public interface ICafeRepository : IRepository<Cafe, CafeId>
{
    //Task<Result<IEnumerable<CafeWithEmployeeCount>>> GetByLocationAsync(string? location, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TProjection>>> GetCafesByLocationAsync<TProjection>(string? location, Expression<Func<Cafe, int, TProjection>> projection, CancellationToken cancellationToken = default);
}
