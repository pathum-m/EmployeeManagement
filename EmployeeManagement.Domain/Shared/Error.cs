namespace EmployeeManagement.Domain.Shared;

public class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error Null = new("Error.Null", "The result is null");
    public static readonly Error NotFound = new("Error.NotFound", "The specified entity was not found");
    public static readonly Error EntityCreationFailure = new("Error.EntityCreationFailure", "The specified entity was not created");

    public Error(string code, string message)
    {
        Code = code; 
        Message = message;
    }

    public bool Equals(Error? other) => throw new NotImplementedException();

    public string Code { get; }
    public string Message { get; }
}
