using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;

namespace EmployeeManagement.Application.Cafes.Commands.Delete;
public record DeleteCafeCommand(CafeId Id) : IRequest<Result<bool>>;
