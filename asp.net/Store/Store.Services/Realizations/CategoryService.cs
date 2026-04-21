using Store.Domain.Entities;
using Store.Repositories.Interfaces;
using Store.Services.Exceptions;
using Store.Services.Interfaces;

namespace Store.Services.Realizations;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
	public Task<IReadOnlyCollection<Category>> GetAllAsync() => repository.GetAllAsync();

	public Task<Category?> GetByIdAsync(Guid id) => repository.GetByIdAsync(id);

	public async Task<Category> CreateAsync(Category category)
	{
		var normalizedName = NormalizeName(category.Name);
		var existingCategories = await repository.GetAllAsync();
		if (existingCategories.Any(x => string.Equals(x.Name, normalizedName, StringComparison.OrdinalIgnoreCase)))
		{
			throw new ConflictException($"Category '{normalizedName}' already exists.");
		}

		category.Name = normalizedName;
		return await repository.AddAsync(category);
	}

	public async Task<Category> UpdateAsync(Guid id, Category category)
	{
		var normalizedName = NormalizeName(category.Name);
		var current = await repository.GetByIdAsync(id);
		if (current is null)
		{
			throw new EntityNotFoundException($"Category with id {id} was not found.");
		}

		var existingCategories = await repository.GetAllAsync();
		if (existingCategories.Any(x => x.Id != id && string.Equals(x.Name, normalizedName, StringComparison.OrdinalIgnoreCase)))
		{
			throw new ConflictException($"Category '{normalizedName}' already exists.");
		}

		current.Name = normalizedName;
		return await repository.UpdateAsync(id, current)
			?? throw new EntityNotFoundException($"Category with id {id} was not found.");
	}

	public async Task DeleteAsync(Guid id)
	{
		try
		{
			var deleted = await repository.DeleteAsync(id);
			if (!deleted)
			{
				throw new EntityNotFoundException($"Category with id {id} was not found.");
			}
		}
		catch (Exception ex) when (ex is not EntityNotFoundException and not ConflictException)
		{
			throw new ConflictException("Category cannot be deleted because it is used by products.");
		}
	}

	private static string NormalizeName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ValidationException("Category name is required.");
		}

		return name.Trim();
	}
}

