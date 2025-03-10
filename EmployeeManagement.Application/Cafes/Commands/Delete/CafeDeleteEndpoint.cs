using EmployeeManagement.Application.Cafes.Commands.Delete;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Cafes;
public partial class CafeEndpoints
{
    public static async Task<IResult> CafeDeleteEndpoint(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        Result<CafeId> idResult = CafeId.Create(id);
        if (idResult.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest, idResult.Error.Message);
        }

        var command = new DeleteCafeCommand
        (
            idResult.Value
        );

        Result<bool> result = await sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest);
        }
        return ApiResult.Success(result.Value);
    }
}
