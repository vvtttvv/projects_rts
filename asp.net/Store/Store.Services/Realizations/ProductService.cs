using Store.Domain.Entities;
using Store.Repositories.Interfaces;
using Store.Services.Exceptions;
using Store.Services.Interfaces;

namespace Store.Services.Realizations;

public class ProductService(IProductRepository repository, ICategoryRepository categoryRepository) : IProductService
{
	public Task<IReadOnlyCollection<Product>> GetAllAsync() => repository.GetAllAsync();

	public Task<Product?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

	public async Task<Product> CreateAsync(Product product)
	{
		await ValidateAsync(product);
		product.Name = product.Name.Trim();
		product.Price = NormalizePrice(product.Price);
		return await repository.AddAsync(product);
	}

	public async Task<Product> UpdateAsync(Guid id, Product product)
	{
		var current = await repository.GetByIdAsync(id);
		if (current is null)
		{
			throw new EntityNotFoundException($"Product with id {id} was not found.");
		}

		await ValidateAsync(product);

		current.Name = product.Name.Trim();
		current.Price = NormalizePrice(product.Price);
		current.CategoryId = product.CategoryId;

		return await repository.UpdateAsync(id, current)
			?? throw new EntityNotFoundException($"Product with id {id} was not found.");
	}

	public async Task DeleteAsync(Guid id)
	{
		try
		{
			var deleted = await repository.DeleteAsync(id);
			if (!deleted)
			{
				throw new EntityNotFoundException($"Product with id {id} was not found.");
			}
		}
		catch (Exception ex) when (ex is not EntityNotFoundException and not ConflictException)
		{
			throw new ConflictException("Product cannot be deleted because it is used by orders.");
		}
	}

	private async Task ValidateAsync(Product product)
	{
		if (string.IsNullOrWhiteSpace(product.Name))
		{
			throw new ValidationException("Product name is required.");
		}

		if (product.Price <= 0)
		{
			throw new ValidationException("Product price must be greater than zero.");
		}

		var category = await categoryRepository.GetByIdAsync(product.CategoryId);
		if (category is null)
		{
			throw new EntityNotFoundException($"Category with id {product.CategoryId} was not found.");
		}
	}

	private static decimal NormalizePrice(decimal price) => decimal.Round(price, 2, MidpointRounding.AwayFromZero);
}