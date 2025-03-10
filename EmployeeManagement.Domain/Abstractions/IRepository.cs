using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.Abstractions;
public interface IRepository<TEntity, TID> where TEntity : class
{
    Task<Result<TEntity>> GetAsync(TID id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void DeleteAsync(TEntity entity);
    Task<Result<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken);
    void UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    //Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
