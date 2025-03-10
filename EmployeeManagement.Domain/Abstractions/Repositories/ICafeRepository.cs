using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using EmployeeManagement.Domain.ValueObjects.DomainResponses;

namespace EmployeeManagement.Domain.Abstractions.Repositories;

public interface ICafeRepository : IRepository<Cafe, CafeId>
{
    Task<Result<IEnumerable<CafeWithEmployeeCount>>> GetByLocationAsync(string? location, CancellationToken cancellationToken = default);
}
