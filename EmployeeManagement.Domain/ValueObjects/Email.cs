using System.Text.RegularExpressions;
using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.ValueObjects;
public record Email
{
    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled);

    private Email(string value) => Value = value;

    public string Value { get; }

    public static Result<Email> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !EmailRegex.IsMatch(value.Trim()))
        {
            return Result.Failure<Email>(DomainError.Employee.InvalidIDFormat);
        }
        return new Email(value.Trim());
    }

    public override string ToString() => Value;
}
