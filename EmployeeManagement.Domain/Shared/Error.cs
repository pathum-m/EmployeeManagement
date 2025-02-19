namespace EmployeeManagement.Domain.Shared;

public class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error Null = new("Error.Null", "The result is null");

    public Error(string code, string message)
    {
        Code = code; 
        Message = message;
    }

    public bool Equals(Error? other) => throw new NotImplementedException();

    public string Code { get; }
    public string Message { get; }
}
