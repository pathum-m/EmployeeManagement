using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.ValueObjects;
public record Gender
{
    public static readonly Gender Male = new(GenderType.Male);
    public static readonly Gender Female = new(GenderType.Female);

    private Gender(GenderType value) => Value = value;

    public GenderType Value { get; }

    public static Result<Gender> FromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Gender>(DomainError.Employee.InvalidGender);
        }

        return value.Trim().ToLower() switch
        {
            "male" => Male,
            "female" => Female,
            //here we can add more gender types if needed, else send error
            _ => Result.Failure<Gender>(DomainError.Employee.InvalidGender)
        };
    }

    public static Result<Gender> FromInt(int value) => value switch
    {
        (int)GenderType.Male => Male,
        (int)GenderType.Female => Female,
        _ => Result.Failure<Gender>(DomainError.Employee.InvalidGender)
    };

    public override string ToString() => Value.ToString();
}
