using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Employees.Queries.Get;
public record GetEmployeesQuery(CafeId? CafeId) : IRequest<Result<List<EmployeeDto>>>;

