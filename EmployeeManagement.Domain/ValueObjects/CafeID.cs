using EmployeeManagement.Domain.DomainErrors;
using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.ValueObjects;
public record CafeId
{
#pragma warning disable CS8618
    private CafeId() { }
#pragma warning restore CS8618

    public CafeId(Guid value) => Value = value;    

    public Guid Value { get; }

    public static Result<CafeId> Create(Guid? value)
    {
        if (value == null || value == Guid.Empty)
        {
            return Result.Failure<CafeId>(DomainError.Employee.InvalidIDFormat);
        }
        return new CafeId(value.Value);
    }

    public static CafeId GenerateID() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}
