using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.PostgreSQL.Seed;

public static class CategorySeeder
{
	public static async Task<Category[]> SeedAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
	{
		if (await dbContext.Categories.AnyAsync(cancellationToken))
		{
			return await dbContext.Categories
				.AsNoTracking()
				.OrderBy(x => x.Name)
				.ToArrayAsync(cancellationToken);
		}

		var categories = new[]
		{
			new Category { Name = "Electronics" },
			new Category { Name = "Books" },
			new Category { Name = "Home" }
		};

		await dbContext.Categories.AddRangeAsync(categories, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
		return categories;
	}
}

