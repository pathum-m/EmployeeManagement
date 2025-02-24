using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.ValueObjects;
public record EmployeeId
{
    private const string PREFIX = "UI";
    private const int TOTAL_LENGTH = 9;

#pragma warning disable CS8618
    private EmployeeId() { }
#pragma warning restore CS8618

    public EmployeeId(string value) => Value = value;

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

    public static EmployeeId GenerateID()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string remainingPart = new string(Enumerable.Repeat(chars, 7)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        return new EmployeeId($"{PREFIX}{remainingPart}");
    }

}
