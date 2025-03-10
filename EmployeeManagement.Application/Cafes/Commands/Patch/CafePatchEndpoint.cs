using EmployeeManagement.Application.Cafes.Commands.Patch;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Cafes;
public partial class CafeEndpoints
{
    public record CafePatchRequest(string Name, string Description, string Location, string? Logo);

    public static async Task<IResult> CafePatchEndpoint(Guid id, CafePatchRequest request, ISender sender, CancellationToken cancellationToken)
    {
        Result<CafeId> idResult = CafeId.Create(id);
        if (idResult.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest, idResult.Error.Message);
        }

        var command = new PatchCafeCommand
        (
            idResult.Value,
            request.Name,
            request.Description,
            request.Location,
            request.Logo
        );

        Result<bool> result = await sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest, result.Error.Message);
        }
        return ApiResult.Success(result.Value);
    }
}
