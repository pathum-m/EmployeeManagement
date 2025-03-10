using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Domain.ValueObjects.DomainResponses;
public class CafeWithEmployeeCount
{

    public CafeWithEmployeeCount()
    {

    }
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string? Logo { get; set; }
    public int EmployeeCount { get; set; }
}

//public record CafeWithEmployeeCount(CafeId Id, string Name, string Description, string Location, string? Logo, int EmployeeCount);
