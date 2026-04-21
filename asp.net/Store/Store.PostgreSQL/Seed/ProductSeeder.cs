using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.PostgreSQL.Seed;

public static class ProductSeeder
{
	public static async Task<Product[]> SeedAsync(AppDbContext dbContext, IReadOnlyList<Category> categories,
		CancellationToken cancellationToken = default)
	{
		if (await dbContext.Products.AnyAsync(cancellationToken))
		{
			return await dbContext.Products
				.AsNoTracking()
				.OrderBy(x => x.Name)
				.ToArrayAsync(cancellationToken);
		}

		var electronics = categories.First(x => x.Name == "Electronics");
		var books = categories.First(x => x.Name == "Books");
		var home = categories.First(x => x.Name == "Home");

		var products = new[]
		{
			new Product { Name = "Laptop", Price = 1299.99m, CategoryId = electronics.Id },
			new Product { Name = "Clean Code", Price = 39.90m, CategoryId = books.Id },
			new Product { Name = "Desk Lamp", Price = 25.50m, CategoryId = home.Id }
		};

		await dbContext.Products.AddRangeAsync(products, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
		return products;
	}
}

