using Domain.Entities;

namespace Repositories.Interfaces;

public interface IOrderRepository
{
	Task<IReadOnlyCollection<Order>> GetAllAsync();
	Task<Order?> GetByIdAsync(int id);
	Task<Order> AddAsync(Order order);
}