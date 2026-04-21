using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Repositories.Interfaces;
using Store.Repositories.Models;

namespace Store.PostgreSQL.Repositories;

public class CategoryRepository(AppDbContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
{
	public override Task<IReadOnlyCollection<Category>> GetAllAsync(QueryOptions<Category>? options = null,
		CancellationToken cancellationToken = default)
	{
		var effectiveOptions = new QueryOptions<Category>
		{
			StartIndex = options?.StartIndex,
			MaxCount = options?.MaxCount,
			AsNoTracking = options?.AsNoTracking ?? true,
			Filter = options?.Filter,
			OrderBy = options?.OrderBy ?? (q => q.OrderBy(x => x.Name)),
			Includes = options?.Includes ?? []
		};

		return base.GetAllAsync(effectiveOptions, cancellationToken);
	}

	public override async Task<Category?> UpdateAsync(Guid id, Category category, CancellationToken cancellationToken = default)
	{
		var existing = await DbContext.Categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		if (existing is null)
		{
			return null;
		}

		existing.Name = category.Name;
		await DbContext.SaveChangesAsync(cancellationToken);
		return existing;
	}
}
