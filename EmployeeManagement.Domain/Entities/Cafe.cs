using EmployeeManagement.Domain.Base;
using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;

namespace EmployeeManagement.Domain.Entities;

public class Cafe : Entity<CafeId>
{
    private readonly List<Employee> _employees = new();

    private Cafe() : base(default!) { }

    private Cafe(
        CafeId id,
        string name,
        string description,
        string location) : base(id)
    {
        Name = name;
        Description = description;
        Location = location;
    }

    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string? Logo { get; set; }
    public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();

    public static Result<Cafe> Create(string name, string description, string location, string? logo = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Result.Failure<Cafe>(DomainError.Cafe.EmptyName);
        }

        var cafe = new Cafe(
            CafeId.GenerateID(),
            name,
            description,
            location)
        {
            Logo = logo
        };

        return cafe;
    }

    public Result<bool> UpdateDetails(string name, string description, string location, string? logo = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Result.Failure<bool>(DomainError.Cafe.EmptyName);
        }

        Name = name;
        Description = description;
        Location = location;
        Logo = logo;

        return true;
    }

    public Result<bool> AddEmployee(Employee employee)
    {
        if (employee.CurrentCafe != null && employee.CurrentCafe != Id)
        {
            return Result.Failure<bool>(DomainError.Cafe.InvalidEmployeeForCafe);
        }

        Result<bool> assignmentResult = employee.AssignToCafe(this, DateTime.UtcNow);
        if (assignmentResult.IsFailure)
        {
            return Result.Failure<bool>(assignmentResult.Error);
        }

        if (!_employees.Contains(employee))
        {
            _employees.Add(employee);
        }
        return true;
    }
}
