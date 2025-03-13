using EmployeeManagement.Domain.Abstractions;
using EmployeeManagement.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure;

public class Repository<TEntity, TID>
    : IRepository<TEntity, TID> where TEntity : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> m_dbset;

    public Repository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        m_dbset = context.Set<TEntity>();
    }

    public async Task<Result<TEntity>> GetAsync(TID id, CancellationToken cancellationToken)
    {
        TEntity? result = await m_dbset.FindAsync(id);
        if (result == null)
        {
            return Result.Failure<TEntity>(Error.NotFound);
        }
        return Result.Success(result);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken) => await m_dbset.AddAsync(entity, cancellationToken);

    public void DeleteAsync(TEntity entity) => m_dbset.Remove(entity);

    public async Task<Result<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken)
    {
        List<TEntity> result = await m_dbset.ToListAsync(cancellationToken);
        if (result == null)
        {
            return Result.Failure<IEnumerable<TEntity>>(Error.NotFound);
        }
        return Result.Success(result as IEnumerable<TEntity>);
    }

    public void UpdateAsync(TEntity entity, CancellationToken cancellationToken) => m_dbset.Update(entity);
}
