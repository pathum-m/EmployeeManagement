using EmployeeManagement.Application.Cafes.Queries.Get;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Cafes;
public partial class CafeEndpoints
{
    public record GetCafesRequest(string Location);

    public static async Task<IResult> GetCafesEndpoint(GetCafesRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetCafesQuery
        (
            request.Location
        );

        Result<List<CafeDto>> result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest);
        }

        return ApiResult.Success(result.Value);
    }
}
