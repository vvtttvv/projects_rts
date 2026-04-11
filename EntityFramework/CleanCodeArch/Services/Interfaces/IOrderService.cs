using Domain.Entities;

namespace Services.Interfaces;

public interface IOrderService
{
	Task<IReadOnlyCollection<Order>> GetAllAsync();
	Task<Order?> GetByIdAsync(int id);
	Task<Order> CreateAsync(Order order);
}