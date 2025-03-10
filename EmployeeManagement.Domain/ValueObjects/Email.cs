using System.Text.RegularExpressions;
using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.ValueObjects;
public record Email
{
    private Email() => Value = string.Empty;

    private Email(string value) => Value = value;

    public string Value { get; }

    public static Result<Email> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValidEmail(value))
        {
            return Result.Failure<Email>(DomainError.Employee.InvalidIDFormat);
        }
        return new Email(value.Trim().ToLower());
    }

    private static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern, RegexOptions.IgnoreCase);
    }
}
