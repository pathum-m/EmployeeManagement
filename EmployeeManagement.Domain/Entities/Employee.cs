using EmployeeManagement.Domain.Base;

namespace EmployeeManagement.Domain.Entities;

internal class Employee : Entity
{
    public Employee(Guid id) : base(id)
    {
    }
}

