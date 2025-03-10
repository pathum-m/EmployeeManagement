using EmployeeManagement.Application.Employees.Commands.Patch;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Employees;
public partial class EmployeeEndpoints
{
    public record EmployeePatchRequest(string Name, string EmailAddress, string PhoneNumber, string Gender, Guid? CafeId);

    public static async Task<IResult> EmployeePatchEndpoint(string id, EmployeePatchRequest request, ISender sender, CancellationToken cancellationToken)
    {
        Result<EmployeeId> idResult = EmployeeId.Create(id);
        if (idResult.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest, idResult.Error.Message);
        }

        Result<Email> emailResult = Email.Create(request.EmailAddress);
        if (emailResult.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest, emailResult.Error.Message);
        }

        Result<PhoneNumber> phoneResult = PhoneNumber.Create(request.PhoneNumber);
        if (phoneResult.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest, phoneResult.Error.Message);
        }

        Result<Gender> genderResult = Gender.FromString(request.Gender);
        if (genderResult.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest, genderResult.Error.Message);
        }

        Result<CafeId> cafeIdResult = CafeId.Create(request.CafeId);

        var command = new PatchEmployeeCommand
        (
            idResult.Value,
            request.Name,
            emailResult.Value,
            phoneResult.Value,
            genderResult.Value,
            cafeIdResult.IsSuccess ? cafeIdResult.Value : null
        );

        Result<bool> result = await sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest);
        }
        return ApiResult.Success(result.Value);
    }
}
