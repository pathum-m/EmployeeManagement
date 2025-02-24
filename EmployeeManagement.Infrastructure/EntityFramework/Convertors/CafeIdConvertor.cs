using EmployeeManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagement.Infrastructure.EntityFramework.Convertors;
public class CafeIdConvertor : ValueConverter<CafeId, Guid>
{
    public CafeIdConvertor() : base(IN => IN.Value,
        OUT => new CafeId(OUT))
    {
    }
}
