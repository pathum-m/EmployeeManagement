using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;

namespace EmployeeManagement.Infrastructure.Repositories;
public class EmployeeRepository : Repository<Employee, EmployeeId>, IEmployeeRespository
{
    public EmployeeRepository(EmployeeDBContext employeeDBContext)
        :base(employeeDBContext)
    {
        
    }

    public Task<Result<IEnumerable<Employee>>> GetByCafeIdAsync(Guid cafeId, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    public Task<Result<bool>> IsEmployeeAssignedToCafeAsync(string employeeId, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
