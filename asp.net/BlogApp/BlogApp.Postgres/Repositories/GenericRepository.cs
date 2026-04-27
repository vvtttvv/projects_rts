using Microsoft.EntityFrameworkCore;
using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using System.Linq.Expressions;

namespace BlogApp.Postgres.Repositories;

public abstract class GenericRepository<T>(AppDbContext dbContext) : IGenericRepository<T>
	where T : BaseEntity
{
	protected readonly AppDbContext DbContext = dbContext;

	public virtual async Task<IReadOnlyCollection<T>> GetAllAsync(int startIndex = 0, int maxCount = 0, 
		List<Expression<Func<T, object>>> includes = null!, bool asNoTracking = true )
	{
		var query = BuildQuery( startIndex, maxCount, asNoTracking, includes);
		return await query.ToListAsync();
	}

	public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
	{
		return await DbContext.Set<T>().AnyAsync(predicate);
	}

	public virtual async Task<T?> GetByIdAsync(Guid id)
	{
		return await DbContext.Set<T>()
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id);
	}

	public virtual async Task<T> AddAsync(T entity)
	{
		await DbContext.Set<T>().AddAsync(entity);
		await SaveChangesWithAuditAsync();
		return entity;
	}

	public virtual async Task<T?> UpdateByIdAsync(Guid id, T entity)
	{
		var existing = await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
		if (existing is null)
		{
			return null;
		}

		DbContext.Entry(existing).CurrentValues.SetValues(entity);
		existing.Id = id;
		await SaveChangesWithAuditAsync();
		return existing;
	}

	public virtual async Task<bool> DeleteAsync(Guid id)
	{
		var existing = await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
		if (existing is null)
		{
			return false;
		}

		DbContext.Set<T>().Remove(existing);
		await SaveChangesWithAuditAsync();
		return true;
	}

	protected IQueryable<T> BuildQuery(int startIndex, int maxCount, bool asNoTracking = true, 
				List<Expression<Func<T, object>>> includes = null! )
	{
		IQueryable<T> query = DbContext.Set<T>();
		if (asNoTracking)
		{
			query = query.AsNoTracking();
		}
		
		foreach (var include in includes ?? [])
		{
			query = query.Include(include);
		}


		if (startIndex > 0)
		{
			query = query.Skip(startIndex);
		}

		if (maxCount > 0)
		{
			query = query.Take(maxCount);
		}

		return query;
	}

	private async Task<int> SaveChangesWithAuditAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in DbContext.ChangeTracker.Entries<BaseEntity>())
		{
			if (entry.State == EntityState.Modified)
			{
				entry.Entity.UpdatedAt = DateTime.UtcNow;
			}
		}

		return await DbContext.SaveChangesAsync(cancellationToken);
	}
}
