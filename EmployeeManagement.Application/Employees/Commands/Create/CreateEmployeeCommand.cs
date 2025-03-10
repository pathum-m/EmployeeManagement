using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.Create;
public record CreateEmployeeCommand(string Name, Email EmailAddress, PhoneNumber PhoneNumber, Gender Gender, CafeId? CafeId) : IRequest<Result<string>>;
