using EmployeeManagement.Domain.Shared;

namespace EmployeeManagement.Domain.Abstractions;
public interface IRepository<TEntity, TID> where TEntity : class
{
    Task<Result<TEntity>> Get(TID id, CancellationToken cancellationToken);
    Task Add(TEntity entity, CancellationToken cancellationToken);
    void Delete(TEntity entity);
    Task<Result<IEnumerable<TEntity>>> GetAll(CancellationToken cancellationToken);
    void Update(TEntity entity, CancellationToken cancellationToken);
}
