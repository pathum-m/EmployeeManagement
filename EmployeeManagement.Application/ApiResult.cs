using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application;
public class ApiResult<T> : ApiResult
{
    public T? Result { get; set; }
    public ApiResult(T result)
        : base(ApiResultCode.Ok) => Result = result;
}

public class ApiResult
{
    public int Status { get; init; }
    public string Type { get; init; }

    public string? Message { get; init; }

    public ApiResult(ApiResultCode statusCode)
    {
        Status = (int)statusCode;
        Type = statusCode.ToString();
    }
    public ApiResult(ApiResultCode statusCode, string message)
    {
        Status = (int)statusCode;
        Type = statusCode.ToString();
        Message = message;
    }

    public static Ok<ApiResult> Success() => TypedResults.Ok(new ApiResult(ApiResultCode.Ok));

    public static Ok<ApiResult<T>> Success<T>(T result) => TypedResults.Ok(new ApiResult<T>(result));

    public static Ok<ApiResult> Failure(ApiResultCode statusCode) => TypedResults.Ok(new ApiResult(statusCode));

    public static Ok<ApiResult> Failure(ApiResultCode statusCode, string message) => TypedResults.Ok(new ApiResult(statusCode, message));
}

public enum ApiResultCode
{
    Ok = 200,
    Created = 201,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    InternalServerError = 500
}
