using EmployeeManagement.Domain.Shared;
using MediatR;

namespace EmployeeManagement.Application.Employees.Queries.Get;
public record GetEmployeesQuery(Guid? CafeId) : IRequest<Result<List<EmployeeDto>>>;

