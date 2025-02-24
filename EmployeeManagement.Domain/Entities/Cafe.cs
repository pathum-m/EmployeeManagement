using EmployeeManagement.Domain.Base;
using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;

namespace EmployeeManagement.Domain.Entities;

public class Cafe : Entity<CafeId>
{
    //private readonly List<Employee> _employees = new();
    //public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();

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

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Location { get; private set; } = null!;
    public string? Logo { get; private set; }

    public static Result<Cafe> Create(
        string name,
        string description,
        string location,
        string? logo = null)
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

    //public void AddEmployee(Employee employee) => _employees.Add(employee);
}
