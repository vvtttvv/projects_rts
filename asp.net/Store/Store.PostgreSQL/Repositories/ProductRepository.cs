using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Repositories.Interfaces;

namespace Store.PostgreSQL.Repositories;

public class ProductRepository(AppDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
{
	public override async Task<Product?> UpdateAsync(Guid id, Product product, CancellationToken cancellationToken = default)
	{
		var existing = await DbContext.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		if (existing is null)
		{
			return null;
		}

		existing.Name = product.Name;
		existing.Price = product.Price;
		existing.CategoryId = product.CategoryId;
		await DbContext.SaveChangesAsync(cancellationToken);
		return existing;
	}
}