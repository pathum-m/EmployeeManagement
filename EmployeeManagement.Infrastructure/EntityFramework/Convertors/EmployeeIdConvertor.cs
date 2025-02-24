using EmployeeManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagement.Infrastructure.EntityFramework.Convertors;
public class EmployeeIdConvertor : ValueConverter<EmployeeId, string>
{
    public EmployeeIdConvertor() : base(IN => IN.Value,
        OUT => new EmployeeId(OUT))
    {
    }
}
