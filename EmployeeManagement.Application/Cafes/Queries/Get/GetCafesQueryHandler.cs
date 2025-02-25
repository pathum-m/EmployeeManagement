using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Cafes.Queries.Get;
public class GetCafesQueryHandler : IRequestHandler<GetCafesQuery, Result<List<CafeDto>>>
{
    private readonly ICafeRepository _cafeRepository;
    private readonly ILogger<GetCafesQueryHandler> _logger;

    public GetCafesQueryHandler(ICafeRepository cafeRepository,
            ILogger<GetCafesQueryHandler> logger)
    {
        _cafeRepository = cafeRepository;
        _logger = logger;
    }

    public async Task<Result<List<CafeDto>>> Handle(GetCafesQuery query, CancellationToken cancellationToken)
    {
        Result<IEnumerable<Domain.Entities.Cafe>> cafesResult = string.IsNullOrEmpty(query.Location)
                   ? await _cafeRepository.GetAllAsync(cancellationToken)
                   : await _cafeRepository.GetByLocationAsync(query.Location, cancellationToken);

        if (cafesResult.IsFailure)
        {
            _logger.LogError("Fetching cafes request has failed: Params {Query}", query);
            return Result.Failure<List<CafeDto>>(Error.NotFound);
        }

        var cafeDtos = cafesResult.Value
            .Select(c => new CafeDto(
                c.Id.Value,
                c.Name,
                c.Description,
                c.Location,
                c.Logo,
                c.Employees?.Count ?? 0
            ))
            .OrderByDescending(c => c.Employees)
            .ToList();

        return Result.Success(cafeDtos);
    }
}
