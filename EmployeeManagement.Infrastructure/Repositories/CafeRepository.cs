using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using EmployeeManagement.Domain.ValueObjects.DomainResponses;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;
public class CafeRepository : Repository<Cafe, CafeId>, ICafeRepository
{
    public CafeRepository(EmployeeDBContext employeeDBContext)
        :base(employeeDBContext)
    {
        
    }

    public async Task<Result<IEnumerable<CafeWithEmployeeCount>>> GetByLocationAsync(string? location, CancellationToken cancellationToken = default)
    {
        string locationParam = string.IsNullOrWhiteSpace(location)
            ? "%"
            : $"%{location.ToLower()}%";

        List<CafeWithEmployeeCount> cafes = await _context.Database
        .SqlQuery<CafeWithEmployeeCount>(@$"SELECT
            c.id,
            c.name,
            c.description,
            c.location,
            c.logo,
            COUNT(e.id) AS employee_count
        FROM cafe c
        LEFT JOIN employee e ON c.id = e.cafe_id
        WHERE LOWER(c.location) LIKE { locationParam }
        GROUP BY c.id, c.name, c.description, c.location, c.logo")
        .ToListAsync();

        return cafes.Any() || !string.IsNullOrWhiteSpace(location)
            ? Result.Success<IEnumerable<CafeWithEmployeeCount>>(cafes)
            : Result.Failure<IEnumerable<CafeWithEmployeeCount>>(Error.NotFound);

    }
}
