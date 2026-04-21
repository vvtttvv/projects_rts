using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Repositories.Interfaces;
using Store.Repositories.Models;

namespace Store.PostgreSQL.Repositories;

public abstract class GenericRepository<TEntity>(AppDbContext dbContext) : IGenericRepository<TEntity>
	where TEntity : BaseEntity
{
	protected readonly AppDbContext DbContext = dbContext;

	public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync(QueryOptions<TEntity>? options = null,
		CancellationToken cancellationToken = default)
	{
		var query = BuildQuery(options);
		return await query.ToListAsync(cancellationToken);
	}

	public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await DbContext.Set<TEntity>()
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
	}

	public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
	{
		await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
		await DbContext.SaveChangesAsync(cancellationToken);
		return entity;
	}

	public virtual async Task<TEntity?> UpdateAsync(Guid id, TEntity entity, CancellationToken cancellationToken = default)
	{
		var existing = await DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		if (existing is null)
		{
			return null;
		}

		DbContext.Entry(existing).CurrentValues.SetValues(entity);
		existing.Id = id;
		await DbContext.SaveChangesAsync(cancellationToken);
		return existing;
	}

	public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
	{
		var existing = await DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		if (existing is null)
		{
			return false;
		}

		DbContext.Set<TEntity>().Remove(existing);
		await DbContext.SaveChangesAsync(cancellationToken);
		return true;
	}

	protected IQueryable<TEntity> BuildQuery(QueryOptions<TEntity>? options)
	{
		IQueryable<TEntity> query = DbContext.Set<TEntity>();
		options ??= new QueryOptions<TEntity>();

		if (options.AsNoTracking)
		{
			query = query.AsNoTracking();
		}

		if (options.Filter is not null)
		{
			query = query.Where(options.Filter);
		}

		foreach (var include in options.Includes)
		{
			query = query.Include(include);
		}

		if (options.OrderBy is not null)
		{
			query = options.OrderBy(query);
		}

		if (options.StartIndex.HasValue && options.StartIndex.Value > 0)
		{
			query = query.Skip(options.StartIndex.Value);
		}

		if (options.MaxCount.HasValue && options.MaxCount.Value > 0)
		{
			query = query.Take(options.MaxCount.Value);
		}

		return query;
	}
}

