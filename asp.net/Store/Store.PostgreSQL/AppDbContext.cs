using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.PostgreSQL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<Category> Categories => Set<Category>();
	public DbSet<Order> Orders => Set<Order>();
	public DbSet<Product> Products => Set<Product>();
	
	public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
	{
		foreach (var entry in ChangeTracker.Entries<BaseEntity>())
		{
			if (entry.State == EntityState.Modified)
				entry.Entity.UpdatedAt = DateTime.UtcNow;
		}
		return await base.SaveChangesAsync(ct);
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}
}