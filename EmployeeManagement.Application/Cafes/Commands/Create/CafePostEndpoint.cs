using EmployeeManagement.Application.Cafes.Commands.Create;
using EmployeeManagement.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Cafes;
public partial class CafeEndpoints
{
    public record CafePost (string Name, string Description, string Location, string? Logo);

    public static async Task<IResult> CafePostEndpoint (CafePost cafePost, ISender sender, CancellationToken cancellationToken) 
    {
        var command = new CreateCafeCommand
        (
            cafePost.Name,
            cafePost.Description,
            cafePost.Location,
            cafePost.Logo
        );

        Result<Guid> result = await sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest);
        }

        return ApiResult.Success(result.Value);
    }
}
