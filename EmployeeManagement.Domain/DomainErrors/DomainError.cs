using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.DomainErrors;

public static class DomainError
{
    public static class Employee
    {
        public static readonly Error InvalidIDFormat = new Error("Employee.InvalidIDFormat", "Employee ID must be in format 'UIXXXXXXX' where X is alphanumeric");
        public static readonly Error InvalidEmail = new Error("Employee.InvalidEmail", "Invalid email format"); 
        public static readonly Error InvalidPhoneNumber = new Error("Employee.InvalidPhoneNumber", "Phone number must start with 8 or 9 and have 8 digits");
        public static readonly Error InvalidGender = new Error("Employee.InvalidGender", "Gender must be either Male or Female");
    }

    public static class Cafe
    {
        public static readonly Error Test = new Error("Cafe.Test", "This is a test error message");
    }
}
