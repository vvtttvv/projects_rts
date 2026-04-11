using Domain.Entities;

namespace Repositories.Interfaces;

public interface IProductRepository
{
	Task<IReadOnlyCollection<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(int id);
	Task<Product> AddAsync(Product product);
}