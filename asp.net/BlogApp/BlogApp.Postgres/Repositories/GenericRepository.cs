using Microsoft.EntityFrameworkCore;
using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using System.Linq.Expressions;

namespace BlogApp.Postgres.Repositories;

public abstract class GenericRepository<T>(AppDbContext dbContext) : IGenericRepository<T>
	where T : BaseEntity
{
	protected readonly AppDbContext DbContext = dbContext;

	public virtual async Task<PagedResult<T>> GetAllAsync(int page = 1, int pageSize = 10,
		List<Expression<Func<T, object>>>? includes = null, bool asNoTracking = true)
	{
		page = page < 1 ? 1 : page;
		pageSize = pageSize < 1 ? 1 : pageSize;

		var query = BuildQuery(asNoTracking, includes);
		var totalCount = await query.CountAsync();
		var items = await query
			.Skip((page - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();

		return new PagedResult<T>(items, page, pageSize, totalCount);
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
		return entity;
	}

	public virtual Task<T?> UpdateByIdAsync(Guid id, T entity)
	{
		entity.Id = id;
		DbContext.Set<T>().Update(entity);
		return Task.FromResult<T?>(entity);
	}

	public virtual async Task<bool> DeleteAsync(Guid id)
	{
		return await DbContext.Set<T>().Where(x => x.Id == id).ExecuteDeleteAsync() > 0;
	}

	public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
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

	protected IQueryable<T> BuildQuery(bool asNoTracking = true,
				List<Expression<Func<T, object>>>? includes = null)
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


		return query;
	}
}
