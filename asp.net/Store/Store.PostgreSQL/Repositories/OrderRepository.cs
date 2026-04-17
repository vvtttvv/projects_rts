using Store.Domain.Entities;
using Store.Repositories.Interfaces;

namespace Store.PostgreSQL.Repositories;

public class OrderRepository : IOrderRepository
{
	private static readonly List<Order> Orders = [];
	private static int _nextId = 1;

	public Task<IReadOnlyCollection<Order>> GetAllAsync() => Task.FromResult<IReadOnlyCollection<Order>>(Orders);

	public Task<Order?> GetByIdAsync(int id) => Task.FromResult(Orders.FirstOrDefault(x => x.Id == id));

	public Task<Order> AddAsync(Order order)
	{
		order.Id = _nextId++;
		Orders.Add(order);
		return Task.FromResult(order);
	}
}