using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Cafes.Commands.Delete;
public record DeleteCafeCommand : IRequest<Result<bool>>
{
    public CafeId Id { get; set; }

}
