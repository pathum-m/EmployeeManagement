using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Cafes.Commands.Patch;
public record PatchCafeCommand(CafeId Id, string Name, string Description, string Location, string? Logo) : IRequest<Result<bool>>;
