using EmployeeManagement.Application.Employees.Commands.Delete;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Employees;
public partial class EmployeeEndpoints
{
    public static async Task<IResult> EmployeeDeleteEndpoint(string id, ISender sender, CancellationToken cancellationToken)
    {
        Result<EmployeeId> employeeIdResult = EmployeeId.Create(id);
        if (employeeIdResult.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest, employeeIdResult.Error.Message);
        }

        var command = new DeleteEmployeeCommand
        (
            employeeIdResult.Value
        );

        Result<bool> result = await sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest);
        }

        return ApiResult.Success(result.Value);
    }
}
