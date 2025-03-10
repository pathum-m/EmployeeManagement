using EmployeeManagement.Application.Employees.Commands.Create;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Employees;
public partial class EmployeeEndpoints
{
    public record EmployeePostRequest(string Name, string EmailAddress, string PhoneNumber, string Gender, Guid? CafeId);

    public static async Task<IResult> EmployeePostEndpoint(EmployeePostRequest request, ISender sender, CancellationToken cancellationToken)
    {
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
            return ApiResult.Failure(ApiResultCode.BadRequest, phoneResult.Error.Message);
        }

        Result<CafeId> cafeIdResult = CafeId.Create(request.CafeId);

        var command = new CreateEmployeeCommand(
            request.Name,
            emailResult.Value,
            phoneResult.Value,
            genderResult.Value,
            cafeIdResult.IsSuccess ? cafeIdResult.Value : null
        );

        Result<string> result = await sender.Send(command, cancellationToken);
        if (result.IsFailure)
        {
            return ApiResult.Failure(ApiResultCode.BadRequest, result.Error.Message);
        }

        return ApiResult.Success(result.Value);
    }
}
