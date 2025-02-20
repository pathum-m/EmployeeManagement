using System.Text.RegularExpressions;
using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.ValueObjects;
public record PhoneNumber
{
    private static readonly Regex PhoneRegex = new(
        @"^[89]\d{7}$",
        RegexOptions.Compiled);

    private PhoneNumber(string value) => Value = value;

    public string Value { get; }

    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !PhoneRegex.IsMatch(value.Trim()))
        {
            return Result.Failure<PhoneNumber>(DomainError.Employee.InvalidPhoneNumber);
        }
        return new PhoneNumber(value.Trim());
    }

    public override string ToString() => Value;
}
