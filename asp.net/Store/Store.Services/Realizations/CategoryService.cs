using Store.Domain.Entities;
using Store.Repositories.Interfaces;
using Store.Services.Interfaces;

namespace Store.Services.Realizations;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
	public Task<IReadOnlyCollection<Category>> GetAllAsync() => repository.GetAllAsync();

	public Task<Category?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

	public async Task<Category> CreateAsync(Category category)
	{
		if (string.IsNullOrWhiteSpace(category.Name))
		{
			throw new ArgumentException("Category name is required.");
		}

		var normalizedName = category.Name.Trim();
		var existingCategories = await repository.GetAllAsync();
		if (existingCategories.Any(x => string.Equals(x.Name, normalizedName, StringComparison.OrdinalIgnoreCase)))
		{
			throw new InvalidOperationException($"Category '{normalizedName}' already exists.");
		}

		category.Name = normalizedName;
		return await repository.AddAsync(category);
	}
}