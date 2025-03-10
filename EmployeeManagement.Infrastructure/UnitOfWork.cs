using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.Abstractions.Repositories;

namespace EmployeeManagement.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly EmployeeDBContext _context;
    public UnitOfWork(
        EmployeeDBContext context,
        IEmployeeRespository employeeRepository,
        ICafeRepository cafeRepository)
    {
        _context = context;

        Employees = employeeRepository;
        Cafes = cafeRepository;
    }

    public IEmployeeRespository Employees { get; private set; }
    public ICafeRepository Cafes { get; private set; }

    public void Dispose() => _context.Dispose();

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) => await _context.SaveChangesAsync(cancellationToken);
}
