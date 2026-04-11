using Domain.Entities;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Realizations;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
	public Task<IReadOnlyCollection<Category>> GetAllAsync() => repository.GetAllAsync();

	public Task<Category?> GetByIdAsync(int id) => repository.GetByIdAsync(id);

	public Task<Category> CreateAsync(Category category) => repository.AddAsync(category);
}