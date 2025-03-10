using EmployeeManagement.Domain.Shared;
using MediatR;

namespace EmployeeManagement.Application.Cafes.Queries.Get;
public record GetCafesQuery(string? Location) : IRequest<Result<List<CafeDto>>>;

