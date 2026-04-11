using Domain.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Realizations;

public class OrderService(IOrderRepository repository) : IOrderService
{
	public Task<IReadOnlyCollection<Order>> GetAllAsync() => repository.GetAllAsync();

	public Task<Order?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

	public Task<Order> CreateAsync(Order order) => repository.AddAsync(order);
}