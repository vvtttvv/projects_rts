using Store.Domain.Entities;

namespace Store.Services.Interfaces;

public interface IOrderService
{
	Task<IReadOnlyCollection<Order>> GetAllAsync();
	Task<Order?> GetByIdAsync(Guid id);
	Task<Order> CreateAsync(Order order);
	Task<Order> UpdateAsync(Guid id, Order order);
	Task DeleteAsync(Guid id);
}