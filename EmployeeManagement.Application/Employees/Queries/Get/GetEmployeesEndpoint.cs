using EmployeeManagement.Application.Employees.Queries.Get;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Employees;
public partial class EmployeeEndpoints
{
    public record EmployeesGetRequest(Guid CafeId);

    public static async Task<IResult> GetEmployeesByCafeEndpoint(EmployeesGetRequest request, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetEmployeesQuery
        (
            request.CafeId
        );

        Result<List<EmployeeDto>> result = await sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest);
        }

        return ApiResult.Success(result.Value);
    }
}
