using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Repositories.Interfaces;

namespace Store.PostgreSQL.Repositories;

public class OrderRepository(AppDbContext dbContext) : GenericRepository<Order>(dbContext), IOrderRepository
{
	public override async Task<Order?> UpdateAsync(Guid id, Order order, CancellationToken cancellationToken = default)
	{
		var existing = await DbContext.Orders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		if (existing is null)
		{
			return null;
		}

		existing.ProductId = order.ProductId;
		existing.Quantity = order.Quantity;
		await DbContext.SaveChangesAsync(cancellationToken);
		return existing;
	}
}