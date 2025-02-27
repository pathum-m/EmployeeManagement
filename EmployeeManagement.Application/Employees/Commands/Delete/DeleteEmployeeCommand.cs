using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.Delete;
public record DeleteEmployeeCommand (EmployeeId Id) : IRequest<Result<bool>>;

