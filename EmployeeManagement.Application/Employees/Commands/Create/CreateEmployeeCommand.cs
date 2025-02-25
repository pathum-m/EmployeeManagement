using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.Create;
public record CreateEmployeeCommand : IRequest<Result<string>>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public CafeId? CafeId { get; set; }
}
