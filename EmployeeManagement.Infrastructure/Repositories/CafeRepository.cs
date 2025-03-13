using System.Linq.Expressions;
using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using EmployeeManagement.Infrastructure.Projections;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;
public class CafeRepository : Repository<Cafe, CafeId>, ICafeRepository
{
    public CafeRepository(EmployeeDBContext employeeDBContext)
        :base(employeeDBContext)
    {
        
    }

    public async Task<Result<IEnumerable<CafeWithEmployeeCount>>> GetByLocationAsync(string? location)
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

    public async Task<Result<IEnumerable<TProjection>>> GetCafesByLocationAsync<TProjection>(string? location, Expression<Func<Cafe, int, TProjection>> projection, CancellationToken cancellationToken = default)
    {
        IQueryable<Cafe> cafeQuery = m_dbset.AsQueryable();

        if (!string.IsNullOrWhiteSpace(location))
        {
            cafeQuery = cafeQuery.Where(c => EF.Functions.Like(c.Location.ToLower(), $"%{location.ToLower()}%"));
        }

        var query = from cafe in cafeQuery
                    join employee in _context.Set<Employee>()
                        on cafe.Id equals employee.CurrentCafe into employeeGroup
                    select new
                    {
                        Cafe = cafe,
                        EmployeeCount = employeeGroup.Count()
                    };

        var results = await query.ToListAsync(cancellationToken);
        var projectedResults = results
            .Select(x => projection.Compile()(x.Cafe, x.EmployeeCount))
            .ToList();

        return projectedResults.Any() || !string.IsNullOrWhiteSpace(location)
            ? Result.Success<IEnumerable<TProjection>>(projectedResults)
            : Result.Failure<IEnumerable<TProjection>>(Error.NotFound);
    }
}
