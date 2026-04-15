using Store.Domain.Entities;
using Store.Repositories.Interfaces;
using Store.Services.Interfaces;

namespace Store.Services.Realizations;

public class ProductService(IProductRepository repository, ICategoryRepository categoryRepository) : IProductService
{
	public Task<IReadOnlyCollection<Product>> GetAllAsync() => repository.GetAllAsync();

	public Task<Product?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

	public async Task<Product> CreateAsync(Product product)
	{
		if (string.IsNullOrWhiteSpace(product.Name))
		{
			throw new ArgumentException("Product name is required.");
		}

		if (product.Price <= 0)
		{
			throw new ArgumentException("Product price must be greater than zero.");
		}

		if (product.CategoryId <= 0)
		{
			throw new ArgumentException("CategoryId must be greater than zero.");
		}

		var category = await categoryRepository.GetByIdAsync(product.CategoryId);
		if (category is null)
		{
			throw new KeyNotFoundException($"Category with id {product.CategoryId} was not found.");
		}

		product.Name = product.Name.Trim();
		product.Price = decimal.Round(product.Price, 2, MidpointRounding.AwayFromZero);
		return await repository.AddAsync(product);
	}
}