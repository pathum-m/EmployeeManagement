using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeeManagement.Infrastructure.EntityFramework.Convertors;

public class DateOnlyConvertor : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConvertor()
        : base(dateOnly =>
                dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
    { }
}

