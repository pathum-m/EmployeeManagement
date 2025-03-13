using System.Linq.Expressions;
using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;
public class EmployeeRepository : Repository<Employee, EmployeeId>, IEmployeeRespository
{
    public EmployeeRepository(EmployeeDBContext employeeDBContext)
        :base(employeeDBContext)
    {
        
    }

    public async Task<Result<IEnumerable<Employee>>> GetByCafeIdAsync(CafeId cafeId, CancellationToken cancellationToken = default)
    {
        List<Employee> employees = await m_dbset
            .Where(e => e.CurrentCafe == cafeId)
            .ToListAsync(cancellationToken);

        return employees.Any() ? Result.Success<IEnumerable<Employee>>(employees) : Result.Failure<IEnumerable<Employee>>(DomainError.Employee.NotFound);
    }

    public async Task<Result<bool>> IsEmployeeAssignedToCafeAsync(EmployeeId employeeId, CancellationToken cancellationToken = default)
    {
        bool exists = await m_dbset.AnyAsync(e => e.Id == employeeId && e.CurrentCafe != null, cancellationToken);
        return Result.Success(exists);
    }

    public async Task<Result<IEnumerable<TProjection>>> GetEmployeesWithCafeNameAsync<TProjection>(CafeId? cafeId, Expression<Func<Employee, string, TProjection>> projection, CancellationToken cancellationToken = default)
    {
        IQueryable<Employee> employeeQuery = m_dbset;

        if (cafeId != null)
        {
            employeeQuery = employeeQuery.Where(e => e.CurrentCafe == cafeId);
        }

        var query = from employee in employeeQuery
                    join cafe in _context.Set<Cafe>()
                        on employee.CurrentCafe equals cafe.Id into cafeGroup
                    from cafe in cafeGroup.DefaultIfEmpty()
                    select new { employee, cafe };

        var results = await query.ToListAsync(cancellationToken);

        var projectedResults = results
                .Select(x =>
                {
                    string cafeName = x.cafe?.Name ?? string.Empty;
                    return projection.Compile()(x.employee, cafeName);
                }).ToList();

        return projectedResults.Any()
            ? Result.Success<IEnumerable<TProjection>>(projectedResults)
            : Result.Failure<IEnumerable<TProjection>>(DomainError.Employee.NotFound);
    }
}
