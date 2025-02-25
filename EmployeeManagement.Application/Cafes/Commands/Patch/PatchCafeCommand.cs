using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Cafes.Commands.Patch;
public record PatchCafeCommand : IRequest<Result<bool>>
{
    public CafeId Id { get; set; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Location { get; init; } = null!;
    public string? Logo { get; init; }
}
