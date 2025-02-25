using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;

namespace EmployeeManagement.Domain.Abstractions.Repositories;

public interface ICafeRepository : IRepository<Cafe, CafeId>
{
    Task<Result<IEnumerable<Cafe>>> GetByLocationAsync(string location, CancellationToken cancellationToken = default);
}
