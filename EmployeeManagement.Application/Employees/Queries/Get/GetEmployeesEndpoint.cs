using EmployeeManagement.Application.Employees.Queries.Get;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Employees;
public partial class EmployeeEndpoints
{
    public static async Task<IResult> GetEmployeesByCafeEndpoint(Guid cafeId, ISender sender, CancellationToken cancellationToken)
    {
        Result<CafeId> idResult = CafeId.Create(cafeId);

        var query = new GetEmployeesQuery
        (
            idResult.IsSuccess ? idResult.Value : null
        );

        Result<List<EmployeeDto>> result = await sender.Send(query, cancellationToken);
        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest);
        }
        return ApiResult.Success(result.Value);
    }
}
