using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.ValueObjects;
public record CafeID
{
    private CafeID(Guid value) => Value = value;

    public static CafeID CreateNew() => new(Guid.NewGuid());

    public Guid Value { get; }

    public static Result<CafeID> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            return Result.Failure<CafeID>(Error.Null);
        }
        return new CafeID(value);
    }
    public override string ToString() => Value.ToString();
}
