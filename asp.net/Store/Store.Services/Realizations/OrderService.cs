using Store.Domain.Entities;
using Store.Repositories.Interfaces;
using Store.Services.Interfaces;

namespace Store.Services.Realizations;

public class OrderService(IOrderRepository repository, IProductRepository productRepository) : IOrderService
{
	public Task<IReadOnlyCollection<Order>> GetAllAsync() => repository.GetAllAsync();

	public Task<Order?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

	public async Task<Order> CreateAsync(Order order)
	{
		if (order.ProductId <= 0)
		{
			throw new ArgumentException("ProductId must be greater than zero.");
		}

		if (order.Quantity <= 0)
		{
			throw new ArgumentException("Quantity must be greater than zero.");
		}

		if (order.Quantity > 100)
		{
			throw new ArgumentException("Quantity cannot be greater than 100 for a single order.");
		}

		var product = await productRepository.GetByIdAsync(order.ProductId);
		if (product is null)
		{
			throw new KeyNotFoundException($"Product with id {order.ProductId} was not found.");
		}

		return await repository.AddAsync(order);
	}
}