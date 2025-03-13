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
    }

    public string Name { get; private set; } = null!;
    public Email EmailAddress { get; private set; } = null!;
    public PhoneNumber PhoneNumber { get; private set; } = null!;
    public Gender Gender { get; private set; } = null!;
    public DateOnly? StartDate { get; private set; }
    public CafeId? CurrentCafe { get; private set; }

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

    public Result<bool> UpdateDetails(string name, Email email, PhoneNumber phone, Gender gender)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Result.Failure<bool>(DomainError.Employee.EmptyName);
        }

        Name = name;
        EmailAddress = email;
        PhoneNumber = phone;
        Gender = gender;

        return true;
    }

    public Result<bool> AssignToCafe(Cafe cafe, DateTime startDate)
    {
        if (CurrentCafe != null && CurrentCafe != cafe.Id)
        {
            return Result.Failure<bool>(DomainError.Employee.EmployeeAssignedForDifferentCafe);
        }
        CurrentCafe = cafe.Id;
        StartDate = DateOnly.FromDateTime(startDate);
        return true;
    }

    public int CalculateDaysWorked()
    {
        if (CurrentCafe == null || !StartDate.HasValue) //If Employee has a cafe Id he must have a start date.
        {
            return 0;
        }

        return (DateTime.UtcNow.Date - StartDate.Value.ToDateTime(TimeOnly.MinValue)).Days;
    }
}

