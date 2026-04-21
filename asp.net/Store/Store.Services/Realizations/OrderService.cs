using Store.Domain.Entities;
using Store.Repositories.Interfaces;
using Store.Services.Exceptions;
using Store.Services.Interfaces;

namespace Store.Services.Realizations;

public class OrderService(IOrderRepository repository, IProductRepository productRepository) : IOrderService
{
	public Task<IReadOnlyCollection<Order>> GetAllAsync() => repository.GetAllAsync();

	public Task<Order?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

	public async Task<Order> CreateAsync(Order order)
	{
		await ValidateAsync(order);
		return await repository.AddAsync(order);
	}

	public async Task<Order> UpdateAsync(Guid id, Order order)
	{
		var current = await repository.GetByIdAsync(id);
		if (current is null)
		{
			throw new EntityNotFoundException($"Order with id {id} was not found.");
		}

		await ValidateAsync(order);

		current.ProductId = order.ProductId;
		current.Quantity = order.Quantity;
		return await repository.UpdateAsync(id, current)
			?? throw new EntityNotFoundException($"Order with id {id} was not found.");
	}

	public async Task DeleteAsync(Guid id)
	{
		var deleted = await repository.DeleteAsync(id);
		if (!deleted)
		{
			throw new EntityNotFoundException($"Order with id {id} was not found.");
		}
	}

	private async Task ValidateAsync(Order order)
	{
		if (order.Quantity <= 0)
		{
			throw new ValidationException("Quantity must be greater than zero.");
		}

		if (order.Quantity > 100)
		{
			throw new ValidationException("Quantity cannot be greater than 100 for a single order.");
		}

		var product = await productRepository.GetByIdAsync(order.ProductId);
		if (product is null)
		{
			throw new EntityNotFoundException($"Product with id {order.ProductId} was not found.");
		}
	}
}