using EmployeeManagement.Domain.Abstractions.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Shared;
using EmployeeManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories;
public class CafeRepository : Repository<Cafe, CafeId>, ICafeRepository
{
    public CafeRepository(EmployeeDBContext employeeDBContext)
        :base(employeeDBContext)
    {
        
    }

    public async Task<Result<IEnumerable<Cafe>>> GetByLocationAsync(string location, CancellationToken cancellationToken = default)
    {
        IQueryable<Cafe> query = m_dbset.AsQueryable();

        if (!string.IsNullOrWhiteSpace(location))
        {
            query = query.Where(c =>
                c.Location.ToLower().Contains(location.ToLower()));
        }

        return await query.ToListAsync(cancellationToken);
    }
}
