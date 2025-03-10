using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.Patch;
public record PatchEmployeeCommand(EmployeeId Id, string Name, Email EmailAddress,
    PhoneNumber PhoneNumber, Gender Gender, CafeId? CafeId) : IRequest<Result<bool>>;
