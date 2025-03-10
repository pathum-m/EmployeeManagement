using EmployeeManagement.Application.Cafes.Queries.Get;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Application.Cafes;
public partial class CafeEndpoints
{
    //If we need morre filter parameters other than location we can use Request record for that
    //public record GetCafesRequest(string Location);

    public static async Task<IResult> GetCafesEndpoint([FromQuery] string? location, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetCafesQuery
        (
            location
        );

        Result<List<CafeDto>> result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest);
        }

        return ApiResult.Success(result.Value);
    }
}
