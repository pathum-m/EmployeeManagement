using EmployeeManagement.Domain.Abstractions.Repositories;

namespace EmployeeManagement.Domain.Abstractions;
public interface IUnitOfWork : IDisposable
{
    IEmployeeRespository Employees { get; }
    ICafeRepository Cafes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
