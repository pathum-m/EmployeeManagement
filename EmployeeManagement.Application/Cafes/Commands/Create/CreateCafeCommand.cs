using EmployeeManagement.Domain.Shared;
using MediatR;

namespace EmployeeManagement.Application.Cafes.Commands.Create;
public record CreateCafeCommand(string Name, string Description, string Location, string? Logo) : IRequest<Result<Guid>>;
