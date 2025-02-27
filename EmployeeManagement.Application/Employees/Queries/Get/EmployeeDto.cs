namespace EmployeeManagement.Application.Employees.Queries.Get;
public record EmployeeDto
(
    string Id,
    string Name,
    string EmailAddress,
    string PhoneNumber,
    string Gender,
    int DaysWorked
);
