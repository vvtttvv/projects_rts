using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.PostgreSQL.Seed;

public static class OrderSeeder
{
	public static async Task SeedAsync(AppDbContext dbContext, IReadOnlyList<Product> products,
		CancellationToken cancellationToken = default)
	{
		if (await dbContext.Orders.AnyAsync(cancellationToken))
		{
			return;
		}

		var laptop = products.First(x => x.Name == "Laptop");
		var cleanCode = products.First(x => x.Name == "Clean Code");

		var orders = new[]
		{
			new Order { ProductId = laptop.Id, Quantity = 1 },
			new Order { ProductId = cleanCode.Id, Quantity = 2 }
		};

		await dbContext.Orders.AddRangeAsync(orders, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
	}
}

