using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Cafes.Queries.Get;
public class GetCafesQueryHandler : IRequestHandler<GetCafesQuery, Result<List<CafeDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetCafesQueryHandler> _logger;

    public GetCafesQueryHandler(IUnitOfWork unitOfWork, ILogger<GetCafesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<List<CafeDto>>> Handle(GetCafesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            //Result<IEnumerable<CafeWithEmployeeCount>> cafesResult = await _unitOfWork.Cafes.GetByLocationAsync(query.Location, cancellationToken);

            Result<IEnumerable<CafeDto>> cafesResult = await _unitOfWork.Cafes.GetCafesByLocationAsync(query.Location,
            (cafe, employeeCount) => new CafeDto
            (
                cafe.Id.Value,
                cafe.Name,
                cafe.Description,
                cafe.Location,
                cafe.Logo,
                employeeCount
            ), cancellationToken);

            if (cafesResult.IsFailure)
            {
                _logger.LogError("Fetching cafes request has failed: Params {Query}", query);
                return Result.Failure<List<CafeDto>>(Error.NotFound);
            }

            var cafeDtos = cafesResult.Value
                .OrderByDescending(c => c.Employees)
                .ToList();

            return Result.Success(cafeDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred when retrieving cafes : {ErrorMessage}", ex.Message);
            return Result.Failure<List<CafeDto>>(new Error("500", ex.Message));
        }
    }
}
