using Store.Domain.Entities;

namespace Store.Services.Interfaces;

public interface IProductService
{
	Task<IReadOnlyCollection<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(Guid id);
	Task<Product> CreateAsync(Product product);
	Task<Product> UpdateAsync(Guid id, Product product);
	Task DeleteAsync(Guid id);
}