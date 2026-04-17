using Store.Domain.Entities;

namespace Store.Repositories.Interfaces;

public interface IOrderRepository
{
	Task<IReadOnlyCollection<Order>> GetAllAsync();
	Task<Order?> GetByIdAsync(int id);
	Task<Order> AddAsync(Order order);
}