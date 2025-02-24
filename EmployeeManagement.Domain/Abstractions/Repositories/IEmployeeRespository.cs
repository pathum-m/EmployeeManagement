using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.ValueObjects;

namespace EmployeeManagement.Domain.Abstractions.Repositories;

public interface IEmployeeRespository : IRepository<Employee, EmployeeId>
{

}
