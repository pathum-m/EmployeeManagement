using EmployeeManagement.Domain.Shared;
using MediatR;

namespace EmployeeManagement.Application.Cafes.Commands.Create;
public record CreateCafeCommand : IRequest<Result<Guid>>
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Location { get; init; } = null!;
    public string? Logo { get; init; }
}
