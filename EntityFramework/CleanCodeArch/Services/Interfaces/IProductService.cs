using Domain.Entities;

namespace Services.Interfaces;

public interface IProductService
{
	Task<IReadOnlyCollection<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(int id);
	Task<Product> CreateAsync(Product product);
}