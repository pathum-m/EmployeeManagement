using System.Linq.Expressions;
using EmployeeManagement.Domain.Enums;
using EmployeeManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagement.Infrastructure.EntityFramework.Convertors;
public class GenderConvertor : ValueConverter<Gender, string>
{
    //public GenderConverter()
    //    : base(
    //        v => v.ToString().ToLower(),
    //        v => {
    //            var result = Gender.FromString(v);
    //            if (result.IsFailure)
    //                throw new InvalidOperationException($"Invalid gender value: {v}");
    //            return result.Value;
    //        })
    //{
    //}
    public GenderConvertor()
            : base(IN => IN.Value.ToString(),
                OUT => Gender.FromString(OUT).Value)
    { }
}
