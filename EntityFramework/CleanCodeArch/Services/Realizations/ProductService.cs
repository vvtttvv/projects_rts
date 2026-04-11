using Domain.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Realizations;

public class ProductService(IProductRepository repository) : IProductService
{
	public Task<IReadOnlyCollection<Product>> GetAllAsync() => repository.GetAllAsync();

	public Task<Product?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

	public Task<Product> CreateAsync(Product product) => repository.AddAsync(product);
}