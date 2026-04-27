using BlogApp.Postgres;
using BlogApp.Postgres.Seed;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.API.Extensions;

public static class ApplicationInitializationExtensions
{
	public static async Task ApplyMigrationsAndSeedingAsync(this WebApplication app, CancellationToken cancellationToken = default)
	{
		using var scope = app.Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

		await dbContext.Database.MigrateAsync(cancellationToken);
		await DbSeeder.SeedAsync(dbContext, cancellationToken);
	}
}

