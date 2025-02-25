using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.ValueObjects;

namespace EmployeeManagement.Infrastructure.Repositories;
public class EmployeeRepository : Repository<Employee, EmployeeId>, IEmployeeRespository
{
    public EmployeeRepository(EmployeeDBContext employeeDBContext)
        :base(employeeDBContext)
    {
        
    }
}
