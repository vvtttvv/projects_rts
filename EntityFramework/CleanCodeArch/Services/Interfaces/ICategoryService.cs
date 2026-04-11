using Domain.Entities;

namespace Services.Interfaces;

public interface ICategoryService
{
	Task<IReadOnlyCollection<Category>> GetAllAsync();
	Task<Category?> GetByIdAsync(int id);
	Task<Category> CreateAsync(Category category);
}