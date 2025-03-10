namespace EmployeeManagement.Application.Cafes.Queries.Get;
public record CafeDto (Guid Id, string Name, string Description, string Location, string? Logo, int Employees);
