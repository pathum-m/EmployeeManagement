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

    public async Task<Result<IEnumerable<Employee>>> GetByCafeIdAsync(CafeId cafeId, CancellationToken cancellationToken)
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
}
