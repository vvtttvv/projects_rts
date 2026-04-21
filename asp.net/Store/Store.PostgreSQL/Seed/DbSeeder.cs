namespace Store.PostgreSQL.Seed;

public static class DbSeeder
{
	public static async Task SeedAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
	{
		var categories = await CategorySeeder.SeedAsync(dbContext, cancellationToken);
		var products = await ProductSeeder.SeedAsync(dbContext, categories, cancellationToken);
		await OrderSeeder.SeedAsync(dbContext, products, cancellationToken);
	}
}

