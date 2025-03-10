using EmployeeManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagement.Infrastructure.EntityFramework.Convertors;
public class GenderConvertor : ValueConverter<Gender, int>
{
    //better to save gender as int
    public GenderConvertor()
        : base(IN => (int)IN.Value,
            OUT => Gender.FromInt(OUT).Value)
    { }
}
