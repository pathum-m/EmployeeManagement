using EmployeeManagement.Application.Cafes.Commands.Create;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Cafes;
public partial class CafeEndpoints
{
    public record CafePostRequest (string Name, string Description, string Location, string? Logo);

    public static async Task<IResult> CafePostEndpoint (CafePostRequest request, ISender sender, CancellationToken cancellationToken) 
    {
        var command = new CreateCafeCommand
        (
            request.Name,
            request.Description,
            request.Location,
            request.Logo
        );

        Result<Guid> result = await sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest);
        }
        return ApiResult.Success(result.Value);
    }
}
