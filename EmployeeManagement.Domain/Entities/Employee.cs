using EmployeeManagement.Domain.Base;
using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;

namespace EmployeeManagement.Domain.Entities;

public class Employee : Entity<EmployeeId>
{
    private Employee() : base(default!) { }

    private Employee(
        EmployeeId id,
        string name,
        Email emailAddress,
        PhoneNumber phoneNumber,
        Gender gender) : base(id)
    {
        Name = name;
        EmailAddress = emailAddress;
        PhoneNumber = phoneNumber;
        Gender = gender;
        //StartDate = DateOnly.FromDateTime(DateTime.UtcNow);
    }

    public string Name { get; private set; } = null!;
    public Email EmailAddress { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public Gender Gender { get; private set; } = null!;
    public DateOnly? StartDate { get; private set; }
    public CafeId? CafeId { get; private set; }

    public static Result<Employee> Create(
        string name,
        Email emailAddress,
        PhoneNumber phoneNumber,
        Gender gender)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Result.Failure<Employee>(DomainError.Employee.EmptyName);
        }
        return new Employee(EmployeeId.GenerateID(), name, emailAddress, phoneNumber, gender);
    }

    //public void AssignToCafe(CafeId cafeId) => CafeId = cafeId;
    public Result<bool> AssignToCafe(Cafe cafe, DateTime startDate)
    {
        if (CafeId != null && CafeId != cafe.Id)
        {
            return Result.Failure<bool>(DomainError.Employee.EmployeeAssignedForDifferentCafe);
        }

        CafeId = cafe.Id;
        StartDate = DateOnly.FromDateTime(startDate);

        return true;
    }

    public int CalculateDaysWorked()
    {
        if (CafeId == null) //If Employee has a cafe Id he must have a start date.
        {
            return 0;
        }

        return (DateTime.UtcNow.Date - StartDate.Value.ToDateTime(TimeOnly.MinValue)).Days;
    }
}

