using Store.Domain.Entities;
using Store.Repositories.Models;

namespace Store.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
	Task<IReadOnlyCollection<TEntity>> GetAllAsync(QueryOptions<TEntity>? options = null, CancellationToken cancellationToken = default);
	Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
	Task<TEntity?> UpdateAsync(Guid id, TEntity entity, CancellationToken cancellationToken = default);
	Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}

