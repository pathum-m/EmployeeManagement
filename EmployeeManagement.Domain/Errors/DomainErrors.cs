using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.Errors;

public static class DomainErrors
{
    public static class Employee 
    {
        public static readonly Error Test = new Error("Employee.Test", "This is a test error message");
    }

    public static class Cafe 
    {
        public static readonly Error Test = new Error("Cafe.Test", "This is a test error message");
    }
}
