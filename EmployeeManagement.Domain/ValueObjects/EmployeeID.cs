using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.ValueObjects;
public record EmployeeId
{
    private const string PREFIX = "UI";
    private const int TOTAL_LENGTH = 9;

    private EmployeeId(string value) => Value = value;

    public string Value { get; }

    public static Result<EmployeeId> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) ||
            value.Length != TOTAL_LENGTH ||
            !value.StartsWith(PREFIX) ||
            !value[2..].All(char.IsLetterOrDigit))
        {
            return Result.Failure<EmployeeId>(DomainError.Employee.InvalidIDFormat);
        }

        return new EmployeeId(value);
    }

    public static EmployeeId CreateNew()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string remainingPart = new string(Enumerable.Repeat(chars, 7)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return new EmployeeId($"{PREFIX}{remainingPart}");
    }

    public override string ToString() => Value;
}
